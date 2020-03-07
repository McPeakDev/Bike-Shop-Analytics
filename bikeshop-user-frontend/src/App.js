import React, {Component} from "react"
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Table from "./Table"
import API from './APIClient'


class App extends Component {
  
  api = new API();

  constructor(props) 
  {
    super(props);
    this.state = {categories: [], obj: {}};
  }
  async componentDidMount() 
  {
    let cats = await this.api.get("category", "readall");
    let plotItemOne = await this.api.get(cats[0].plotItemOne, "readall");
    let plotItemTwo = await this.api.get(cats[0].plotItemTwo, "readall");
    this.setState({ categories: cats, obj: {name: `${cats[0].plotItemOne} vs ${cats[0].plotItemTwo}`, x: plotItemOne , y: plotItemTwo }});
  }

  changeCategory = async(event) => 
  {
    let plotItemOne = await this.api.get(this.state.categories[event.value].plotItemOne, "readall");
    let plotItemTwo = await this.api.get(this.state.categories[event.value].plotItemTwo, "readall"); 
    this.setState({obj: {name: `${this.state.categories[event.value].plotItemOne} vs ${this.state.categories[event.value].plotItemTwo}`, x: plotItemOne, y: plotItemTwo}});
  }

  getKeys() 
  {
      return Object.keys(this.state.categories);
  }

  render()
  {
    let i = 0;
    let keys = this.getKeys();
    let optionItems = keys.map((key) => 
    {
            let objKeys = Object.keys(this.state.categories[key]);
            return <option value={i++} key={key}>{this.state.categories[key][objKeys[1]].capitalize()}</option>
    });
    return(
      <div className="wrapper container">
        <Table selectedObj={this.state.obj} />
        <div className="row">
          <select className="rounded form-control-md col-lg-6" onChange={e => this.changeCategory(e.target)}>{optionItems}</select>
        </div>
      </div>
    )
  }
}

/*eslint no-extend-native: ["error", { "exceptions": ["String"] }]*/
String.prototype.capitalize = function() {
  return this.charAt(0).toUpperCase() + this.slice(1);
}



export default App;
