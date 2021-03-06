import React from 'react';
import { Toast, Button, Form} from 'react-bootstrap';
 
 class Delete extends React.Component
 {
    api = null;

    constructor(props)
    {
        super(props);
        this.api = this.props.api;
        this.state = { data: this.props.data, categoryID: null, categoryIndex: 0, status: false};
    }

    changeCategory = (event) =>
    {
        event.preventDefault();
        this.setState({categoryID: this.state.data[event.target.value].categoryID, categoryIndex: event.target.value});
    }

    async componentWillMount() 
    {
        let cats = await this.api.get("category", "readall");
        if(cats.length > 0)
        {
            this.setState( {data: cats, categoryID: cats[0].categoryID, categoryIndex: 0});
        }
        else
        {
            this.setState( {data: cats});
        }

    }

    deleteCategory = async (event, value) =>
    {
        event.preventDefault();
        let json = await this.api.deleteID("category", value);
        this.setState({status: json.includes("Success!")});
        this.refs.categories.value = 0;
        await this.componentWillMount();
        await this.props.updateLinks()
    }

    resetForm = (event) =>
    {
        event.preventDefault();
        this.refs.categories.value = 0;
        this.setState({xItem: "Bike", yItem: "Bike", chartType: "Bar", status: false})
    }

    conditionalReturnCategories()
    {
        if(this.state.data !== null && this.state.data.length > 0)
        {
            return (
                <div className="row">
                    <div className="col-md-3 col-md-offset-3"></div>
                    <div className="col-md-6 col-md-offset-3">
                        <p className="text-white text-center">=============================</p>
                        <h1 className="text-white text-center"> Delete</h1>
                        <p className="text-white text-center">=============================</p>
                        <form className="text-white" >
                                <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={this.changeCategory}>{this.optionItems()}</Form.Control>
                                <br/>
                                <div className="text-center">              
                                    <Button type="button" onClick={e => this.deleteCategory(e, this.state.categoryID)}>Submit</Button>{' '}
                                    <Button type="button" variant="secondary"  onClick={this.resetForm}>Reset</Button>
                                </div>
                        </form>
                        <br/>
                        <Toast className="mx-auto"  show={this.state.status} onClose={() => this.setState({status: false})}>
                            <Toast.Header>
                                <strong className="mr-auto">Success!</strong>
                                <small>{new Date().toLocaleTimeString()}</small>
                            </Toast.Header>
                            <Toast.Body>Category Deleted!</Toast.Body>
                        </Toast>
                    </div>           
                </div>
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

 export default Delete