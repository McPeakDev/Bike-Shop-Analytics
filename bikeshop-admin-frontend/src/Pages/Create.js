import React from 'react';
import { Button, Form, Col, Toast} from 'react-bootstrap';
 
 class Create extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: null, xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false};
    }

    async componentDidMount()
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
        }
        this.refs.categories.value = 0;
        await this.componentDidMount();
    }

    resetForm = (event) =>
    {
        event.preventDefault();
        this.refs.categories.value = 0;
        this.setState({xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false});
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
                <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{this.optionItems()}</Form.Control>
            )
        }

    }

    optionItems()
    {
        if(this.state.data !== null)
        {
            let i = 0;
            let keys = Object.keys(this.state.data);
            return keys.map((key) => 
            {
                let objKeys = Object.keys(this.state.data[key]);
                return <option value={i++} key={key}>{this.state.data[key][objKeys[1]].capitalize()}</option>
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
                                <Button type="button"  onClick={this.createCategory}>Submit</Button>{' '}
                                <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                            </div>
                        </Form>
                        <br/>
                        <Toast show={this.state.status} onClose={() => this.setState({status: false})} >
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