//Imports for App.js
import React, {Component} from "react"
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Table from "./Table"
import API from './APIClient'
import Chart from './Chart'

//Default Program. Renders all other components
class App extends Component {
  
  //Create a class-wide API variable 
  api = new API();
  UserToken = "9be8a0fbd8e3605bebba0555f14467d1";

  //Default Constructor. Instantiates the state of the class.
  constructor(props) 
  {
    super(props);
    this.state = {
      categories: [],
      obj: {},
      xVals: [],
      yVals: [],
      backgroundColor:[]};
    this.api.Token = this.UserToken;
    this.getColors = this.getColors.bind(this)
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

    this.setState({backgroundColor:[], xVals:[], yVals:[]})

    console.log('Got Here')
  }

  //Get the keys of the applications categories.
  getKeys() 
  {
      return Object.keys(this.state.categories);
  }

  getXandYValues()
  {
    for(let i = 0; i < this.state.obj.y.length; i++)
    {
      for(let j = 0; j < this.state.obj.x.length; j++)
      {
        if(this.state.obj.x[j].bikeID === this.state.obj.y[i].bikeID)
        {
          this.state.xVals.push(this.state.obj.x[j].name)
          break
        }
      }
      this.state.yVals.push(this.state.obj.y[i].salePrice)
    }
  }

  getColors()
    {
        let r = Math.floor(Math.random() * 255);
        let g = Math.floor(Math.random() * 255);
        let b = Math.floor(Math.random() * 255);
        return "rgb(" + r + "," + g + "," + b + ")";
    }

    setColors()
    {
      let c;
      for(let i = 0; i < this.state.obj.x.length; i++)
      {
        c = this.getColors()
        this.state.backgroundColor.push(c)
      }
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
    
    if(this.state.obj.x != undefined && this.state.obj.y != undefined)
    {
      this.getXandYValues()
      this.setColors()
      console.log(this.state.backgroundColor)
    }

    console.log(this.state.obj.x)
    console.log(this.state.obj.y)
    //Return the html content.
    return(
      <div className="wrapper container">
        <div className="row">
          <select className="align-content-center rounded form-control-lg col-lg-12" onChange={e => this.changeCategory(e.target)}>{optionItems}</select>
          <br />
        </div>
        {/*{<Table selectedObj={this.state.obj} />}*/}
        <div>
          <Chart xVals={this.state.xVals} yVals={this.state.yVals} backgroundColor={this.state.backgroundColor} />
        </div>
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
export default App;
