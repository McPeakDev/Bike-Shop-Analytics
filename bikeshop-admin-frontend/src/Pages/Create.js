import React from 'react';
import { Button, Form, Col, Toast} from 'react-bootstrap';
 
 class Create extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: this.props.data, xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false, xProperties: [], yProperties: []};
    }

    async componentWillMount()
    {
        let cats = await this.api.get("category", "readall");
        this.setProperties(this.state.xItem, this.state.yItem, this.state.chartType,  cats)
    }

    createCategory = async (event) =>
    {
        event.preventDefault();
        if(event.value !== "")
        {
            let x = ""
            let y = ""

            this.state.xProperties.forEach(prop => {
                if(prop !== "")
                {
                    x += `${prop},`
                }
            });

            this.state.yProperties.forEach(prop => {
                if(prop !== "")
                {
                    y += `${prop},`
                }
            });

            let chart = {categoryName: `${this.state.xItem} vs ${this.state.yItem}`, xCategory: this.state.xItem, xProperties: x, yCategory: this.state.yItem, yProperties: y, chartType: this.state.chartType, startRange: new Date(), endRange: new Date()};
            
            let json = await this.api.post("category","create", chart);
            
            this.setState({status: json["categoryID"] !== undefined});
            
            await this.props.updateLinks()
        }
        if(this.state.data > 0)
        {
            this.refs.categories.value = 0;
        }
        await this.componentWillMount();
        this.setProperties("Bike", "Bike", "Bar", this.state.data)
    }

    resetForm = (event) =>
    {
        event.preventDefault();
        if(this.state.data > 0)
        {
            this.refs.categories.value = 0;
        }
        this.setProperties("Bike", "Bike", "Bar", this.state.data)
    }

    handlePlotItemX = async (event) =>
    {
        event.preventDefault();
        this.setProperties(event.target.value, this.state.yItem, this.state.chartType, this.state.data)
    }

    handlePlotItemXProperties = async (event) =>
    {
        event.preventDefault();
        let val = event.target.value
        if(!this.state.xProperties.includes(val))
        {
            this.state.xProperties.push(val)
        }
        else
        {
            let index = this.state.xProperties.indexOf(val)
            this.state.xProperties[index] = ""
        }
    }

    handlePlotItemY = async (event) =>
    {
        event.preventDefault();
        this.setProperties(this.state.xItem, event.target.value, this.state.chartType, this.state.data)
    }

    handlePlotItemYProperties = (event) =>
    {
        event.preventDefault();
        let val = event.target.value
        if(!this.state.yProperties.includes(val))
        {
            this.state.yProperties.push(val)
        }
        else
        {
            let index = this.state.yProperties.indexOf(val)
            let props =  this.state.yProperties;
            props[index] = ""
            this.setState({yProperties: props})
        }        
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

    async setProperties(xVal, yVal, chart, data)
    {
        let xInfo = await this.api.get(xVal, "ReadAll")
        let yInfo = await this.api.get(yVal, "ReadAll")


        if(xInfo !== undefined && yInfo !== undefined)
        {

            let xProps = Object.keys(xInfo[0]);
            let yProps = Object.keys(yInfo[0]);


            for (let i = 0; i < xProps.length; i++) 
            {   
                if(!(yProps.includes(xProps[i])))
                {
                        xProps[i] = undefined
                }
            }
            
            let xCategories = this.getSubCategories(xProps)
            let yCategories = this.getSubCategories(yProps)

            this.setState({data: data, xItem: xVal, yItem: yVal, chartType: chart, xCategories: xCategories, yCategories: yCategories, xProperties: [], yProperties: []});
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
            let values = data.map((key) => 
            {
                if(key !== undefined)
                {
                    return <option value={key} key={key}>{key.capitalize()}</option>
                }
                else
                {
                    return undefined
                }
            });

            return values
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
                                <Form.Label className="text-white">Category X</Form.Label>
                                <Form.Control as="select" value={this.state.xItem} onChange={this.handlePlotItemX}>
                                    <option value="Bike">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturerTransaction">Manufacturer Transactions</option>    
                                    <option value="PurchaseOrder">Purchase Orders</option> 
                                    <option value="PurchaseItem">Purchase Items</option> 
                                </Form.Control>
                            </Form.Group>
                            {this.state.xCategories !== undefined &&
                                <Form.Group  as={Col}>
                                  <Form.Label className="text-white">X Properties to Graph</Form.Label>
                                    <Form.Control multiple as="select" value={this.state.xProperties} onChange={this.handlePlotItemXProperties}>
                                        {this.state.xCategories}
                                    </Form.Control>
                                </Form.Group>
                            }
                            <Form.Group  as={Col}>
                                <Form.Label className="text-white">Category Y</Form.Label>
                                <Form.Control as="select" value={this.state.yItem} onChange={this.handlePlotItemY}>
                                    <option value="Bike">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturerTransaction">Manufacturer Transactions</option>   
                                    <option value="PurchaseOrder">Purchase Orders</option> 
                                    <option value="PurchaseItem">Purchase Items</option> 
                                </Form.Control>
                            </Form.Group>
                            {this.state.yCategories !== undefined &&
                                <Form.Group  as={Col}>
                                 <Form.Label className="text-white">Y Properties to Graph</Form.Label>
                                    <Form.Control multiple as="select" value={this.state.yProperties} onChange={this.handlePlotItemYProperties}>
                                        {this.state.yCategories}
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