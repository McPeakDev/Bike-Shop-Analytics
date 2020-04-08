//Imports for App.js
import React, {Component} from "react"
import {Fade, Form} from 'react-bootstrap';
import API from '../Services/APIClient'
import Chart from './Chart'

//Default Program. Renders all other components
class App extends Component {
  
  //Create a class-wide API variable 
  api = new API();

  //Default Constructor. Instantiates the state of the class.
  constructor(props) 
  {
    super(props);
    this.state = {
      categories: [],
      obj: {},
      xVals: [],
      yVals: [],
      backgroundColor:[],
      chartShown: false};
    //this.getColors = this.getColors.bind(this)
  }

  //Upon a component mounting, Load data into the application state.
  async componentWillMount() 
  {
    //Request the categories.
    let cats = await this.api.get("category", "readall");

    //Request the categories X Values.
    let plotItemOne = await this.api.get(cats[0].xCategory, "readall");

    //Request the categories Y Values.
    let plotItemTwo = await this.api.get(cats[0].yCategory, "readall");

    //Assign the variables to their appropriate state objects.
    this.setState({ categories: cats, obj: {name: `${cats[0].plotItemOne} vs ${cats[0].plotItemTwo}`, x: plotItemOne , y: plotItemTwo}, chartShown: true});

    this.initializeChartComponents();

  }

  //Asynchronous event handler. Upon the Category being changed update the table data.
  changeCategory = async(event) => 
  {
    //Request the categories X Values.
    let plotItemOne = await this.api.get(this.state.categories[event.value].xCategory, "readall");

    //Request the categories Y Values.
    let plotItemTwo = await this.api.get(this.state.categories[event.value].yCategory, "readall"); 

    //Assign the variables to their appropriate state objects.
    this.setState({obj: {name: `${this.state.categories[event.value].xCategory} vs ${this.state.categories[event.value].yCategory}`, x: plotItemOne, y: plotItemTwo, backgroundColor:[], xVals:[], yVals:[]}, chartShown: false});

    this.initializeChartComponents();
  }

  initializeChartComponents()
  {
    if(this.state.obj.x !== undefined && this.state.obj.y !== undefined)
    {
      this.getXandYValues()
    }
  }

  //Get the keys of the applications categories.
  getKeys() 
  {
      return Object.keys(this.state.categories);
  }

  getXandYValues()
  {
      let xVals = []
      let yVals = []

    for(let i = 0; i < this.state.obj.y.length; i++)
    {
      for(let j = 0; j < this.state.obj.x.length; j++)
      {
        if(this.state.obj.x[j].bikeID === this.state.obj.y[i].bikeID)
        {
          xVals.push(this.state.obj.x[j].name)
          break
        }
      }
      yVals.push(this.state.obj.y[i].salePrice)
    }

    this.setState({xVals: xVals, yVals: yVals});
  }

  // getColors()
  // {
  //   let r = Math.floor(Math.random() * 255);
  //   let g = Math.floor(Math.random() * 255);
  //   let b = Math.floor(Math.random() * 255);
  //   return "rgb(" + r + "," + g + "," + b + ")";
  // }

  // setColors()
  // {
  //   let c;
  //   for(let i = 0; i < this.state.obj.x.length; i++)
  //   {
  //     c = this.getColors()
  //     this.state.backgroundColor.push(c)
  //   }
  // }

  optionItems()
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

    return optionItems;
  }


  //Render the application.
  render()
  { 
    //Return the html content.
    return(
      <div className="wrapper container">
        <div className="row">
        <div className="col-md- col-md-offset-3"></div>
          <div className="col-md-12 col-md-offset-3">
            <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={(e) => this.changeCategory(e.target)}>{this.optionItems()}</Form.Control>
          </div>
          <br />
        </div>
        {this.state.xVals.length > 0 && this.state.yVals.length > 0 &&
          <Fade onExited={() => this.setState({chartShown: true})} in={this.state.chartShown}>
            <div className="chart-wrapper">
              <Chart xVals={this.state.xVals} yVals={this.state.yVals} />
            </div>
          </Fade>
        }
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
