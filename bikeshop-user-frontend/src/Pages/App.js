//Imports for App.js
import React, {Component} from "react"
import {Fade, Form, Jumbotron} from 'react-bootstrap';
import API from '../Services/APIClient'
import {Line, Bar, defaults} from 'react-chartjs-2';

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
      chartShown: false,
      chartType: null,};
    defaults.global.defaultFontColor = "#ffffff";
  }

  //Upon a component mounting, Load data into the application state.
  async componentWillMount() 
  {
    //Request the categories.
    let cats = await this.api.get("category", "readall");
    if(cats.length > 0)
    {
      //Request the categories X Values.
      let plotItemOne = await this.api.get(cats[0].xCategory, "readall");

      //Request the categories Y Values.
      let plotItemTwo = await this.api.get(cats[0].yCategory, "readall");

      //Assign the variables to their appropriate state objects.
      this.setState({ categories: cats, obj: {name: `${cats[0].plotItemOne} vs ${cats[0].plotItemTwo}`, x: plotItemOne , y: plotItemTwo}, chartShown: true, yLabel: cats[0].yCategory, xLabel: cats[0].xCategory});

      this.initializeChartComponents(0);
      this.getChartData();
    }

  }

  //Asynchronous event handler. Upon the Category being changed update the table data.
  changeCategory = async(event) => 
  {
    //Request the categories X Values.
    let plotItemOne = await this.api.get(this.state.categories[event.value].xCategory, "readall");

    //Request the categories Y Values.
    let plotItemTwo = await this.api.get(this.state.categories[event.value].yCategory, "readall"); 

    //Assign the variables to their appropriate state objects.
    this.setState({obj: {name: `${this.state.categories[event.value].xCategory} vs ${this.state.categories[event.value].yCategory}`, x: plotItemOne, y: plotItemTwo, backgroundColor:[], xVals:[], yVals:[]}, chartShown: false, yLabel: this.state.categories[event.value].yCategory, xLabel: this.state.categories[event.value].xCategory});

    let index = event.value

    this.initializeChartComponents(index);
    this.getChartData();
  }

  initializeChartComponents = (index) =>
  {
    if(this.state.obj.x !== undefined && this.state.obj.y !== undefined)
    {
      this.getXandYValues(index)
    }
    this.getChartType(index)
  }

  create2DArray(yValsLength)
  {
    let yVals = []
    for(let i = 0; i < yValsLength; i++)
    {
      yVals.push([])
    }
    return yVals
  }

  //Get the keys of the applications categories.
  getKeys() 
  {
      return Object.keys(this.state.categories);
  }

  getChartType(index)
  {
    let chart = this.state.categories[index].chartType
    this.setState({chartType: chart})
  }

  getXandYValues(index)
  {
    let xVals = [];
    let yVals = [];

    let xProp = this.state.categories[index].xProperties
    let yProps = this.state.categories[index].yProperties.split(",")

    yProps.pop()

    for (let i = 0; i < yProps.length; i++) 
    {
      yVals.push([])
    }


    this.getValidMappings(index)

    let mapProp; 

    for (let i = 0; i < this.state.validMappings.length; i++) {
      if(this.state.validMappings[i].includes("ID"))
      {
        mapProp = this.state.validMappings[i]
        break;
      }
      else if(this.state.validMappings[i].includes("Date"))
      {
        mapProp = this.state.validMappings[i]
        break;      
      }
      else
      {
        mapProp = this.state.validMappings[0]
        break;
      }        
    }

    for (let i = 0; i < this.state.obj.x.length; i++) 
    {
      for (let j = 0; j < this.state.obj.y.length; j++) 
      {
        if(this.state.obj.y[j][mapProp] === this.state.obj.x[i][mapProp])
        {
          if(mapProp.includes("Date"))
          {
            let index = this.state.obj.x[i][xProp].indexOf("T")
            xVals.push(this.state.obj.x[i][xProp].slice(0, index))
          }
          else
          {
            xVals.push(this.state.obj.x[i][xProp])
          }

          for (let k = 0; k < yProps.length; k++) 
          {
            yVals[k].push(this.state.obj.y[j][yProps[k]])
          } 
          break
        }
      }
    }

    this.setState({xVals: xVals, yVals: yVals, yValueLabels: yProps});
  }

  getValidMappings(index)
  {
    let validMappings = []

    let xVals = [];
    let yVals = [];

    let xProp = this.state.categories[index].xProperties
    let yProps = this.state.categories[index].yProperties.split(",")

    let xAllProps = Object.keys(this.state.obj.x[0])
    let yAllProps = Object.keys(this.state.obj.y[0])

    yProps.pop();

    for (let i = 0; i < xAllProps.length; i++) {
      if(yAllProps.includes(xAllProps[i]))
      {
        if(xProp !== xAllProps[i])
        {
          xVals.push(xAllProps[i])
        }
      }
    }

    for (let i = 0; i < yAllProps.length; i++) {
      if(xAllProps.includes(yAllProps[i]))
      {
        yVals.push(yAllProps[i])
      }
    }

    for (let i = 0; i < yVals.length; i++) {
      if(xVals[i] === yVals[i] && !xVals[i].includes("discount"))
      {
        validMappings.push(xVals[i])
      }
      if(validMappings.length === 0)
      {
        validMappings.push(xProp)
      }
    }

    this.setState({validMappings: validMappings})
  }

  create2DDataset()
    {
        let datasets = []

        for(let i = 0; i < this.state.yValueLabels.length; i++)
        {
            let r = Math.floor(Math.random() * 255)
            let g = Math.floor(Math.random() * 255)
            let b = Math.floor(Math.random() * 255)

            let object = {
                label: this.state.yValueLabels[i].capitalize(),                
                backgroundColor: `rgb(${r}, ${g}, ${b})`,
                borderColor: `rgb(${r}, ${g}, ${b})`,
                fill: false,
                data: []
            }
            datasets.push(object)
        }
        return datasets
    }

    getChartData()
    {
        let chartD = {
            labels: this.state.xVals,
            datasets: null
        }
        let datasets = this.create2DDataset()
            for(let j = 0; j < this.state.yVals[0].length; j++)
            {
                for(let i = 0; i < datasets.length; i++)
                {
                    datasets[i].data.push(this.state.yVals[i][j])
                }
            }
        chartD.datasets = datasets
        this.setState({chartData: chartD})
    }

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
        {this.state.categories.length === 0 &&
          <Jumbotron className="bg-dark">
              <h1 className="text-center text-white">No Data</h1>
          </Jumbotron>
        }
        {this.state.categories.length > 0 &&
          <div className="row">
          <div className="col-md- col-md-offset-3"></div>
            <div className="col-md-12 col-md-offset-3">
              <Form.Control as="select" ref="categories" className="align-content-center rounded form-control-lg col-lg-12" onChange={(e) => this.changeCategory(e.target)}>{this.optionItems()}</Form.Control>
            </div>
            <br />
          </div>
        }
          {this.state.xVals.length > 0 && this.state.yVals.length > 0 && this.state.chartType !== null &&
            <Fade onExited={() => this.setState({chartShown: true})} in={this.state.chartShown}>
              <div className="chart-wrapper">
              {this.state.chartType === 'Line' && 
                  <Line data={this.state.chartData} 
                  options={{
                      scales:{
                        xAxes:[{
                          scaleLabel: {
                            display:true,
                            labelString: this.state.xLabel
                        },
                        }],
                          yAxes:[{
                              scaleLabel: {
                                  display:true,
                                  labelString: this.state.yLabel
                              },
                              ticks: {
                                  suggestedMin: 0
                              }
                          }]
                      }
                  }}/>
                  }
                  {this.state.chartType === 'Bar' && 
                  <Bar 
                  data={this.state.chartData}
                  options={{
                      scales:{
                          xAxes:[{
                            scaleLabel: {
                              display:true,
                              labelString: this.state.xLabel
                          },
                          }],
                          yAxes:[{
                              scaleLabel: {
                                  display:true,
                                  labelString: this.state.yLabel
                              },
                              ticks: {
                                  suggestedMin: 0
                              }
                          }]
                      }
                  }}
                  />
                  }        
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
