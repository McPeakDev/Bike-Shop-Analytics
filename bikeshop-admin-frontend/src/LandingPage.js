import React from 'react';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';


 class LandingPage extends React.Component
 {
    api = null;
    
    constructor(props)
    {
       super(props);
       this.api = this.props.api;
       this.state = { data: [], selectedObj: 0}
    }

    async componentDidMount() 
    {
        this.refs.nameCreate.value = null;
        this.refs.xCreate.value = null;
        this.refs.yCreate.value = null;
        this.refs.chartTypeCreate.value = null;

        let cats = await this.api.get("category", "readall")
        this.setState( {data: cats})

        this.refs.iDUpdate.value = this.state.data[0].categoryID
        this.refs.nameUpdate.value = this.state.data[0].categoryName
        this.refs.xUpdate.value = this.state.data[0].plotItemOne
        this.refs.yUpdate.value = this.state.data[0].plotItemTwo
        this.refs.chartTypeUpdate.value = this.state.data[0].chartType
    }
   
    getKeys() 
    {
        return Object.keys(this.state.data);
    }

    changeCategory = (event) =>
    {
        this.refs.iDUpdate.value = this.state.data[event.target.value].categoryID
        this.refs.nameUpdate.value = this.state.data[event.target.value].categoryName
        this.refs.xUpdate.value = this.state.data[event.target.value].plotItemOne
        this.refs.yUpdate.value = this.state.data[event.target.value].plotItemTwo
        this.refs.chartTypeUpdate.value = this.state.data[event.target.value].chartType
        
    }

    createCategory = async (event) =>
    {
        event.preventDefault();
        var chart = {categoryName: this.refs.nameCreate.value, plotItemOne: this.refs.xCreate.value, plotItemTwo: this.refs.yCreate.value, chartType: this.refs.chartTypeCreate.value}
        await this.api.post("category","create", chart)
        this.componentDidMount();
    }

    updateCategory = async (event) =>
    {   
        event.preventDefault();
        var chart = {categoryID: parseInt(this.refs.iDUpdate.value), categoryName: this.refs.nameUpdate.value, plotItemOne: this.refs.xUpdate.value, plotItemTwo: this.refs.yUpdate.value, chartType: this.refs.chartTypeUpdate.value}
        await this.api.update("category", chart)
        this.componentDidMount();

    }

    deleteCategory = async (event) =>
    {
        event.preventDefault();
        await this.api.deleteID("category", this.state.selectedObj.categoryID)
        this.componentDidMount();
        
    }
   
    render() 
    {
        let i = 0;
        let keys = this.getKeys();
        let optionItems = keys.map((key) => 
        {
            let objKeys = Object.keys(this.state.data[key]);
            return <option value={i++} key={key}>{this.state.data[key][objKeys[1]].capitalize()}</option>
        });
        return (
            <div className="wrapper container">
                <div className="row">
                    <select className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{optionItems}</select>
                    <br />
                    <div className="col-4">
                        <form className="text-white" onSubmit={this.createCategory}>
                            <h1>Create</h1>
                            Name: <input ref="nameCreate" type="text"></input>
                            <br />
                            X Value: <input ref="xCreate" type="text"></input>
                            <br />
                            Y Value: <input ref="yCreate" type="text"></input>
                            <br />                 
                            chartType: <input ref="chartTypeCreate" type="text"></input>
                            <br />
                            <br />
                            <input type="submit"/>
                        </form>
                    </div>
                    <div className="col-4">
                        <form className="text-white"  onSubmit={this.updateCategory}>
                            <h1>Update</h1>
                            <input ref="iDUpdate" type="hidden"></input>
                            Name: <input ref="nameUpdate" type="text"></input>
                            <br />
                            X Value: <input ref="xUpdate" type="text"></input>
                            <br />
                            Y Value: <input ref="yUpdate" type="text"></input>
                            <br />                 
                            chartType: <input ref="chartTypeUpdate" type="text"></input>
                            <br />
                            <input type="submit"/>
                        </form>
                    </div>
                    <div className="col-4">
                        <form className="text-white"  onSubmit={this.deleteCategory}>
                            <h1>Delete</h1>
                            <select className="align-content-center rounded form-control-lg col-lg-12" onChange={e=> this.setState( {selectedObj: this.state.data[e.target.value]})}>{optionItems}</select>
                            <br />
                            <br />
                            <input type="submit"/>
                        </form>
                    </div>
                </div>
            </div>
        )
    }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
  }

export default LandingPage