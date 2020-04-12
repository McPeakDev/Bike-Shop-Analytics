import React from 'react';
import {Alert, Toast, Button, Form, Col} from 'react-bootstrap';
 
 class Update extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: this.props.data, categoryID: null, xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false, xProperty: "", yProperties: [], error: 200}
    }

    async componentWillMount()
    {
        let cats = await this.api.get("category", "readall")
        if(cats.length > 0)
        {
            let yProps = cats[0].yProperties.split(",")
            yProps.pop();

            this.setProperties(cats[0].categoryID, cats[0].xCategory, cats[0].xProperties, cats[0].yCategory, yProps, cats[0].chartType, cats)
        }
        else
        {
            this.setState( {data: cats})
        }
    }

    updateCategory = async (event) =>
    {   
        if(this.state.xProperty.length > 0 && this.state.yProperties.length > 0)
        {
            event.preventDefault();

            let y = ""

            this.state.yProperties.forEach(prop => {
                if(prop !== "")
                {
                    y += `${prop},`
                }
            });

            let chart = {categoryID: this.state.categoryID, categoryName: `${this.state.xItem} vs ${this.state.yItem}` , xCategory: this.state.xItem, xProperties: this.state.xProperty, yCategory: this.state.yItem, yProperties: y, chartType: this.state.chartType, startRange: this.state.startRange, endRange: this.state.endRange}
            let json = await this.api.update("category", chart)
            this.setState({status: json.includes("Success!")});
            this.refs.categories.value = 0;
            await this.props.updateLinks()
        }
        else
        {
            this.setState({error: 400})
        }
        //await this.componentWillMount()
    }

    changeCategory = (event) =>
    {
        event.preventDefault();
        let yProps = this.state.data[event.target.value].yProperties.split(",")
        yProps.pop();
        this.setProperties(this.state.data[event.target.value].categoryID, this.state.data[event.target.value].xCategory, this.state.data[event.target.value].xProperties, this.state.data[event.target.value].yCategory, yProps, this.state.data[event.target.value].chartType, this.state.data)

    }

    resetForm = (event) =>
    {
        event.preventDefault();

        let yProps = this.state.data[0].yProperties.split(",")
        yProps.pop();

        this.setProperties(this.state.data[0].categoryID, this.state.data[0].xCategory, this.state.data[0].xProperties, this.state.data[0].yCategory, yProps, this.state.data[0].chartType, this.state.data)}

    handlePlotItemX = (event) =>
    {
        event.preventDefault();
        this.setProperties(this.state.categoryID, event.target.value, this.state.xProperty, this.state.yItem, this.state.yProperties, this.state.chartType, this.state.data)
    }

    handlePlotItemXProperties = async (event) =>
    {
        event.preventDefault();
        let val = event.target.value
        if(this.state.xProperty !== val)
        {
            this.setState({xProperty: val})
        }
    }

    handlePlotItemY = (event) =>
    {
        event.preventDefault();
        this.setProperties(this.state.categoryID, this.state.xItem, this.state.xProperty, event.target.value, this.state.yProperties, this.state.chartType, this.state.data)
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

    displayerrors()
    {
        if(this.state.error !== 200)
        {
            return <Alert dismissible variant="danger" onClose={() => this.setState({error: 200})}>Form is not complete</Alert>
        }

    }

    conditionalReturnCategories()
    {
        if(this.state.data !== null && this.state.data.length > 0)
        {
            return (
                <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{this.optionItems()}</Form.Control>
            );
        }

    }

    async setProperties(id, xVal, xProp, yVal, yProps, chart, data)
    {
        let xInfo = await this.api.get(xVal, "ReadAll")
        let yInfo = await this.api.get(yVal, "ReadAll")


        if(xInfo !== undefined && yInfo !== undefined)
        {

            let xAllProps = Object.keys(xInfo[0]);
            let yAllProps = Object.keys(yInfo[0]);

            if(xVal !== yVal)
            {
                let invalidMappings = []
                    
                for (let i = 0; i < yAllProps.length; i++) 
                {   
                    if(!(xAllProps.includes(yAllProps[i])))
                    {
                        invalidMappings.push(yAllProps[i])
                    }
                    else
                    {
                        if(!yAllProps[i].includes("Price"))
                        {
                            yAllProps[i] = undefined
                        }
                    }
                }

                console.log(invalidMappings)
                console.log(yAllProps)

                if(invalidMappings.length === yAllProps.length)
                {
                    yAllProps = undefined
                }
            }

            if(xVal === yVal)
            {
                for (let i = 0; i < yAllProps.length; i++) 
                {   
                    if((!xAllProps.includes(yAllProps[i])))
                    {
                            yAllProps[i] = undefined
                    }
                }
            }


            let xCategories = this.getSubCategories(xAllProps)
            let yCategories = this.getSubCategories(yAllProps)

            this.setState({categoryID: id, data: data, xItem: xVal, yItem: yVal, chartType: chart, xCategories: xCategories, yCategories: yCategories, xProperty: xProp, yProperties: yProps});
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
        console.log(this.state)
        return (
            <div>
                <div>
                    {this.displayerrors()}
                    <div className="row">
                        <div className="col-md-3 col-md-offset-3"></div>
                        <div className="col-md-6 col-md-offset-3">
                            <p className="text-white text-center">=============================</p>
                            <h1 className="text-white text-center"> Update</h1>
                            <p className="text-white text-center">=============================</p>
                            {this.conditionalReturnCategories()}
                        </div>           
                    </div>
                    <div className="row">
                        <div className="col-md-4 col-md-offset-3"></div>
                        <div className="col-md-4 col-md-offset-3">
                            <Form ref="UpdateForm">
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
                                    <Form.Label className="text-white">X Property to Graph</Form.Label>
                                        <Form.Control as="select" value={this.state.xProperty} onChange={this.handlePlotItemXProperties}>
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
                                    <Button type="button" onClick={this.updateCategory}>Submit</Button>{' '}
                                    <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                                </div>
                            </Form>
                            <br/>
                            <Toast className="mx-auto" show={this.state.status} onClose={() => this.setState({status: false})} >
                                <Toast.Header>
                                    <strong className="mr-auto">Success!</strong>
                                    <small>{new Date().toLocaleTimeString()}</small>
                                </Toast.Header>
                                <Toast.Body>Category Updated!</Toast.Body>
                            </Toast>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
 }

 export default Update