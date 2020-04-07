import React from 'react';
import { Button, Form, Col, Toast} from 'react-bootstrap';
import { MultiSelect } from 'react-bootstrap-multiselect';
 
 class Create extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: this.props.data, xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false};
    }

    async componentWillMount()
    {
        let cats = await this.api.get("category", "readall");
        this.setState( {data: cats});
    }

    createCategory = async (event) =>
    {
        event.preventDefault();
        if(event.value !== "")
        {
            let chart = {categoryName: `${this.state.xItem} vs ${this.state.yItem}` , plotItemOne: this.state.xItem, plotItemTwo: this.state.yItem, chartType: this.state.chartType};
            let json = await this.api.post("category","create", chart);
            this.setState({status: json["categoryID"] !== undefined});
            await this.props.updateLinks()
        }
        if(this.state.data > 0)
        {
            this.refs.categories.value = 0;
        }
        await this.componentWillMount();
    }

    resetForm = (event) =>
    {
        event.preventDefault();
        if(this.state.data > 0)
        {
            this.refs.categories.value = 0;
        }
        this.setState({xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false});
    }

    handlePlotItemX = async (event) =>
    {
        event.preventDefault();
        let val = event.target.value
        this.setState({xItem: val, xCategories: this.getSubCategories((await this.api.get(val, "ReadAll"))[0])});
    }

    handlePlotItemXProperties = async (event) =>
    {
        event.preventDefault();
        let options = event.target.options
        let xcats = [];
        let val = event.target.value
        for (let i = 0; i < options.length; i++) 
        {
            if(options[i].value === val)
            {
                options[i].selected = "selected"
                console.log(options[i].selected)
            }
            xcats.push(options[i])
        }
        this.setState({xCategories: xcats})
        console.log(this.state.xCategories)
        //await this.componentWillMount();
    }

    handlePlotItemY = async (event) =>
    {
        event.preventDefault();
        var val = event.target.value
        this.setState({yItem: val, yCategories: this.getSubCategories((await this.api.get(val, "ReadAll"))[0])});
    }

    handlePlotItemYProperties = (event) =>
    {
        this.setState({yCategories: event.target.value})
    }

    handleChartType = (event) =>
    {
        event.preventDefault();
        this.setState({chartType: event.target.value});
    }

    conditionalReturnCategories()
    {
        if(this.state.data !== undefined && this.state.data.length > 0)
        {
            return (
                <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{this.optionItems(this.state.data)}</Form.Control>
            )
        }

    }

    optionItems(data)
    {
        if(data !== null && data !== undefined)
        {
            let i = 0;
            let keys = Object.keys(data);
            return keys.map((key) => 
            {
                let objKeys = Object.keys(data[key]);
                return <option value={i++} key={key}>{data[key][objKeys[1]].capitalize()}</option>
            });
        }
        return

    }

    getSubCategories(data)
    {
        if(data !== null && data !== undefined)
        {
            let i = 0;
            let keys = Object.keys(data);
            return keys.map((key) => 
            {
                return <option value={i++} key={key}>{key.capitalize()}</option>
            });
        }
        return

    }

    render()
    {
        return (
            <div>
                <div className="row">
                    <div className="col-md-3 col-md-offset-3"></div>
                    <div className="col-md-6 col-md-offset-3">
                        <p className="text-white text-center">=============================</p>
                        <h1 className="text-white text-center"> Create</h1>
                        <p className="text-white text-center">=============================</p>
                        {this.conditionalReturnCategories()}
                    </div>           
                </div>
                <div className="row">
                    <div className="col-md-4 col-md-offset-3"></div>
                    <div className="col-md-4 col-md-offset-3">
                        <Form ref="CreateForm">
                            <Form.Group  as={Col}>
                                <Form.Label className="text-white">Category for the X Plane</Form.Label>
                                <Form.Control as="select" value={this.state.xItem} onChange={this.handlePlotItemX}>
                                    <option value="Bikes">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                    <option value="PurchaseOrders">PurchaseOrders</option> 
                                    <option value="PurchaseItems">PurchaseItems</option> 
                                </Form.Control>
                            </Form.Group>
                            {this.state.xCategories !== undefined &&
                                <Form.Group  as={Col}>
                                 <Form.Label className="text-white">X Properies to Graph</Form.Label>
                                    <MultiSelect  data={this.state.xCategoriesS} onChange={this.handlePlotItemXProperties}/>
                                </Form.Group>
                            }
                            <Form.Group  as={Col}>
                                <Form.Label className="text-white">Category for the Y Plane</Form.Label>
                                <Form.Control as="select" value={this.state.yItem} onChange={this.handlePlotItemY}>
                                    <option value="Bikes">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                    <option value="PurchaseOrders">PurchaseOrders</option> 
                                    <option value="PurchaseItems">PurchaseItems</option> 
                                </Form.Control>
                            </Form.Group>
                            {this.state.yCategories !== undefined &&
                                <Form.Group  as={Col}>
                                 <Form.Label className="text-white">Y Properies to Graph</Form.Label>
                                    <Form.Control multiple as="select" value={this.state.yCategories} onChange={this.handlePlotItemYProperties}>
                                        {this.getSubCategories(this.state.yCategories)}
                                    </Form.Control>
                                </Form.Group>
                            }              
                            <Form.Group  as={Col}>
                                <Form.Label className="text-white">Chart Type</Form.Label>
                                <Form.Control as="select" value={this.state.chartType} onChange={this.handleChartType}>
                                    <option value="Bar">Bar</option>    
                                    <option value="Line">Line</option>    
                                    <option value="Pie">Pie</option>    
                                </Form.Control>
                            </Form.Group>
                            <div className="text-center">              
                                <Button type="button"  onClick={this.createCategory}>Submit</Button>{' '}
                                <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                            </div>
                        </Form>
                        <br/>
                        <Toast className="mx-auto" show={this.state.status} onClose={() => this.setState({status: false})} >
                            <Toast.Header>
                                <strong className="mr-auto">Success!</strong>
                                <small>{new Date().toLocaleTimeString()}</small>
                            </Toast.Header>
                            <Toast.Body>Category Created!</Toast.Body>
                        </Toast>
                    </div>
                </div>
            </div>
        );
    }
 }

 export default Create