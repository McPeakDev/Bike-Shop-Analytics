//Imports for App.js
import React, {Component} from "react"
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Table from "./Table"
import API from './APIClient'

//Default Program. Renders all other components
class UserPage extends Component {
  
  //Create a class-wide API variable 
  api = new API();

  //Default Constructor. Instantiates the state of the class.
  constructor(props) 
  {
    super(props);
    this.state = {categories: [], obj: {}};
  }

  //Upon a component mounting, Load data into the application state.
  async componentDidMount() 
  {
    //Request the categories.
    let cats = await this.api.get("category", "readall");

    //Request the categories X Values.
    let plotItemOne = await this.api.get(cats[0].plotItemOne, "readall");

    //Request the categories Y Values.
    let plotItemTwo = await this.api.get(cats[0].plotItemTwo, "readall");

    //Assign the variables to their appropriate state objects.
    this.setState({ categories: cats, obj: {name: `${cats[0].plotItemOne} vs ${cats[0].plotItemTwo}`, x: plotItemOne , y: plotItemTwo }});
  }

  //Asynchronous event handler. Upon the Category being changed update the table data.
  changeCategory = async(event) => 
  {
    //Request the categories X Values.
    let plotItemOne = await this.api.get(this.state.categories[event.value].plotItemOne, "readall");

    //Request the categories Y Values.
    let plotItemTwo = await this.api.get(this.state.categories[event.value].plotItemTwo, "readall"); 

    //Assign the variables to their appropriate state objects.
    this.setState({obj: {name: `${this.state.categories[event.value].plotItemOne} vs ${this.state.categories[event.value].plotItemTwo}`, x: plotItemOne, y: plotItemTwo}});
  }

  //Get the keys of the applications categories.
  getKeys() 
  {
      return Object.keys(this.state.categories);
  }

  //Render the application.
  render()
  {
    //Instantiate a counter variable.
    let i = 0;
    let keys = this.getKeys();

    //Create a list of options for a select box.
    let optionItems = keys.map((key) => 
    {
            let objKeys = Object.keys(this.state.categories[key]);
            return <option value={i++} key={key}>{this.state.categories[key][objKeys[1]].capitalize()}</option>
    });
    //Return the html content.
    return(
      <div className="wrapper container">
        <div className="row">
          <select className="align-content-center rounded form-control-lg col-lg-12" onChange={e => this.changeCategory(e.target)}>{optionItems}</select>
          <br />>
        </div>
        <Table selectedObj={this.state.obj} />
      </div>
    )
  }
}

//Custom Capitalize function for strings.
/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
  return this.charAt(0).toUpperCase() + this.slice(1);
}


//Export App for importing.
export default UserPage;
