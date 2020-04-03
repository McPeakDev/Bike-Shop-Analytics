import React from 'react';
import {Toast, Button, Form, Col} from 'react-bootstrap';
 
 class Update extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: null, categoryID: null, xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false}
    }

    async componentDidMount()
    {
        let cats = await this.api.get("category", "readall")
        if(cats.length > 0)
        {
            this.setState( {data: cats, categoryID: cats[0].categoryID, xItem: cats[0].plotItemOne, yItem: cats[0].plotItemTwo, chartType: cats[0].chartType})
        }
        else
        {
            this.setState( {data: cats})
        }
    }

    updateCategory = async (event) =>
    {   
        event.preventDefault();
        let chart = {categoryID: this.state.categoryID, categoryName: `${this.state.xItem} vs ${this.state.yItem}` , plotItemOne: this.state.xItem, plotItemTwo: this.state.yItem, chartType: this.state.chartType}
        let json = await this.api.update("category", chart)
        this.setState({status: json.includes("Success!")});
        this.refs.categories.value = 0;
        await this.componentDidMount()

    }

    changeCategory = (event) =>
    {
        event.preventDefault();
        this.setState({categoryID: this.state.data[event.target.value].categoryID, xItem: this.state.data[event.target.value].plotItemOne, yItem: this.state.data[event.target.value].plotItemTwo, chartType: this.state.data[event.target.value].chartType});
    }

    resetForm = (event) =>
    {
        event.preventDefault();
        this.refs.categories.value = 0;
        this.setState({xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false})
    }

    handlePlotItemX = (event) =>
    {
        event.preventDefault();
        this.setState({xItem: event.target.value});
    }

    handlePlotItemY = (event) =>
    {
        event.preventDefault();
        this.setState({yItem: event.target.value});
    }

    handleChartType = (event) =>
    {
        event.preventDefault();
        this.setState({chartType: event.target.value});
    }

    conditionalReturnCategories()
    {
        if(this.state.data !== null && this.state.data.length > 0)
        {
            return (
                <div>
                    <div className="row">
                        <div className="col-md-3 col-md-offset-3"></div>
                        <div className="col-md-6 col-md-offset-3">
                            <p className="text-white text-center">=============================</p>
                            <h1 className="text-white text-center"> Update</h1>
                            <p className="text-white text-center">=============================</p>
                            <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{this.optionItems()}</Form.Control>
                        </div>           
                    </div>
                    <div className="row">
                        <div className="col-md-4 col-md-offset-3"></div>
                        <div className="col-md-4 col-md-offset-3">
                            <Form ref="UpdateForm">
                                <Form.Group  as={Col}>
                                    <Form.Label className="text-white">X Value</Form.Label>
                                    <Form.Control as="select" value={this.state.xItem} onChange={this.handlePlotItemX}>
                                        <option value="Bikes">Bikes</option>    
                                        <option value="SalesOrder">Sales Orders</option>    
                                        <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                        <option value="PurchaseOrders">PurchaseOrders</option> 
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group  as={Col}>
                                    <Form.Label className="text-white">Y Value</Form.Label>
                                    <Form.Control as="select" value={this.state.yItem} onChange={this.handlePlotItemY}>
                                        <option value="Bikes">Bikes</option>    
                                        <option value="SalesOrder">Sales Orders</option>    
                                        <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                        <option value="PurchaseOrders">PurchaseOrders</option> 
                                    </Form.Control>
                                </Form.Group>              
                                <Form.Group  as={Col}>
                                    <Form.Label className="text-white">Chart Type</Form.Label>
                                    <Form.Control as="select" value={this.state.chartType} onChange={this.handleChartType}>
                                        <option value="Bar">Bar</option>    
                                        <option value="Line">Line</option>    
                                        <option value="Pie">Pie</option>    
                                        <option value="Polar">Polar</option>   
                                    </Form.Control>
                                </Form.Group>              
                                <div className="text-center">
                                <Button type="button" onClick={this.updateCategory}>Submit</Button>{' '}
                                <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                            </div>
                            </Form>
                            <br/>
                            <Toast show={this.state.status} onClose={() => this.setState({status: false})} >
                                <Toast.Header>
                                    <strong className="mr-auto">Success!</strong>
                                    <small>{new Date().toLocaleTimeString()}</small>
                                </Toast.Header>
                                <Toast.Body>Category Updated!</Toast.Body>
                            </Toast>
                        </div>
                    </div>
                </div>
            );
        }
        else
        {
            return(
                <h1 className="text-white text-center">No Data</h1>
            );
        }

    }

    optionItems()
    {
        let i = 0;
        let keys = Object.keys(this.state.data);
        return keys.map((key) => 
        {
            let objKeys = Object.keys(this.state.data[key]);
            return <option value={i++} key={key}>{this.state.data[key][objKeys[1]].capitalize()}</option>
        });
    }

    render()
    {
        return (
            <div>
                {this.conditionalReturnCategories()}
            </div>
        );
    }
 }

 export default Update