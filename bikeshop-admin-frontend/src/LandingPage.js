import React from 'react';
import { Button, Form, Col } from 'react-bootstrap';

 class LandingPage extends React.Component
 {
    api = null;
    
    constructor(props)
    {
       super(props);
       this.api = this.props.api;
       this.state = { data: [], xItem: "Bike", yItem: "Bike", chartType: "Bar"}
    }

    async componentDidMount() 
    {
        this.refs.xCreate.value = null;
        this.refs.yCreate.value = null;
        this.refs.chartTypeCreate.value = null;

        let cats = await this.api.get("category", "readall")
        this.setState( {data: cats})

        if (cats.length > 0)
        {
            this.refs.iDUpdate.value = this.state.data[0].categoryID
            this.refs.xUpdate.value = this.state.data[0].plotItemOne
            this.refs.yUpdate.value = this.state.data[0].plotItemTwo
            this.refs.chartTypeUpdate.value = this.state.data[0].chartType
        }
    }
   
    getKeys() 
    {
        return Object.keys(this.state.data);
    }

    changeCategory = (event) =>
    {
        this.refs.iDUpdate.value = this.state.data[event.target.value].categoryID
        this.refs.xUpdate.value = this.state.data[event.target.value].plotItemOne
        this.refs.yUpdate.value = this.state.data[event.target.value].plotItemTwo
        this.refs.chartTypeUpdate.value = this.state.data[event.target.value].chartType
        
    }

    handlePlotItemX = (e) =>
    {
        console.log(e.target.value);
        this.setState({xItem: e.target.value});
    }

    handlePlotItemY = (e) =>
    {
        console.log(e.target.value);
        this.setState({yItem: e.target.value});
    }

    handleChartType = (e) =>
    {
        console.log(e.target.value);
        this.setState({chartType: e.target.value});
    }

    createCategory = async (event) =>
    {
        event.preventDefault();
        console.log(event.target.value)
        if(event.value != "")
        {
            let chart = {categoryName: `${this.state.xItem} vs ${this.state.yItem}` , plotItemOne: this.state.xItem, plotItemTwo: this.state.yItem, chartType: this.state.chartType}
            await this.api.post("category","create", chart)
            this.componentDidMount();
        }
    }

    updateCategory = async (event) =>
    {   
        event.preventDefault();
        let chart = {categoryID: parseInt(this.state.categoryID), categoryName: `${this.refs.xUpdate.value} vs ${this.refs.yUpdate.value}`, plotItemOne: this.refs.xUpdate.value, plotItemTwo: this.refs.yUpdate.value, chartType: this.refs.chartTypeUpdate.value}
        await this.api.update("category", chart)
        this.componentDidMount();

    }

    deleteCategory = async (event, value) =>
    {
        event.preventDefault();
        let result = await this.api.deleteID("category", value)
        this.componentDidMount();
        
    }
   
    render() 
    {
        let i = 0;
        let keys = this.getKeys();
        let optionItems = keys.map((key) => 
        {
            let objKeys = Object.keys(this.state.data[key]);
            return <option value={this.state.data[key][objKeys[0]]} key={key}>{this.state.data[key][objKeys[1]].capitalize()}</option>
        });
        if (this.state.data.length > 0)
        {
            return (
                <div className="container">
                    <div className="row">
                        <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{optionItems}</Form.Control>
                        <br />
                        <div className="col-sm-4">
                            <Form ref="CreateForm">
                                <h1 className="text-white">Create</h1>
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
                                <Button type="button" onClick={this.createCategory}>Submit</Button>
                            </Form>
                        </div>
                        <div className="col-4">
                            <Form ref="UpdateForm">
                                <h1 className="text-white">Update</h1>
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
                                <Button type="button" onClick={this.updateCategory}>Submit</Button>
                            </Form>

                            {/* <form className="text-white"  onSubmit={this.updateCategory}>
                                <h1>Update</h1>
                                <input ref="iDUpdate" type="hidden"></input>
                                <br />
                                X Value: 
                                <select ref="xUpdate" type="text">
                                    <option value="Bikes">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                    <option value="PurchaseOrders">PurchaseOrders</option>    
                                </select>
                                <br />
                                Y Value: 
                                <select ref="yUpdate" type="text">                               
                                    <option value="Bikes">Bikes</option>    
                                    <option value="SalesOrder">Sales Orders</option>    
                                    <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                    <option value="PurchaseOrders">PurchaseOrders</option>    
                                </select>
                                <br />                 
                                chartType: 
                                <select ref="chartTypeUpdate" type="text">                        
                                    <option value="Bar">Bar</option>    
                                    <option value="Line">Line</option>    
                                    <option value="Pie">Pie</option>    
                                    <option value="Polar">Polar</option>    
                                </select>
                                <br />
                                <Button type="submit">Submit</Button>
                            </form> */}
                        </div>
                        <div className="col-4">
                            <form className="text-white"  onSubmit={e => this.deleteCategory(e, this.refs.categoryDelete.value)}>
                                <h1>Delete</h1>
                                <select ref="categoryDelete" className="align-content-center rounded form-control-lg col-lg-12" >{optionItems}</select>
                                <br />
                                <br />
                                <Button type="submit">Submit</Button>
                            </form>
                        </div>
                    </div>
                </div>
            )
        }
        else
        {
            return (
                <div className="wrapper container">
                    <div className="row">
                        <div className="col-4">
                            <Form onSubmit={this.createCategory}>
                                    <h1 className="text-white">Create</h1>
                                    <Form.Group  as={Col}>
                                        <Form.Label className="text-white">X Value</Form.Label>
                                        <Form.Control as="select" ref="xCreate">
                                            <option value="Bikes">Bikes</option>    
                                            <option value="SalesOrder">Sales Orders</option>    
                                            <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                            <option value="PurchaseOrders">PurchaseOrders</option> 
                                        </Form.Control>
                                    </Form.Group>
                                    <Form.Group  as={Col}>
                                        <Form.Label className="text-white">Y Value</Form.Label>
                                        <Form.Control as="select" ref="yCreate">
                                            <option value="Bikes">Bikes</option>    
                                            <option value="SalesOrder">Sales Orders</option>    
                                            <option value="ManufacturingTransactions">Manufacturing Transactions</option>    
                                            <option value="PurchaseOrders">PurchaseOrders</option> 
                                        </Form.Control>
                                    </Form.Group>              
                                    <Form.Group  as={Col}>
                                        <Form.Label className="text-white">Chart Type</Form.Label>
                                        <Form.Control as="select" ref="chartTypeCreate">
                                            <option value="Bar">Bar</option>    
                                            <option value="Line">Line</option>    
                                            <option value="Pie">Pie</option>    
                                            <option value="Polar">Polar</option>   
                                        </Form.Control>
                                    </Form.Group>              
                                    <Button type="submit">Submit</Button>
                                </Form>
                        </div>
                    </div>
                </div>
            )
        }
    }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
  }

export default LandingPage