import React, {Component} from "react"
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Table from "./Table"
import API from './APIClient'


class App extends Component {
  constructor(props) 
  {
    super(props)
    this.state = {categories: [], xvalues: [], yvalues: [], obj: {}};
    this.changeTable = this.changeCategory.bind(this);

  }
  async componentDidMount() 
  {
      let api = new API();
      this.setState({ categories: await api.get("category", "readall")});
      console.log(this.state.categories);   
  }

  async changeCategory(event) 
  {
    console.log(this.state.categories[event.value])
    let api = new API();
    this.setState({ xvalues: await api.get(this.state.categories[event.value].plotItemOne, "readall")});   
    this.setState({ yvalues: await api.get(this.state.categories[event.value].plotItemTwo, "readall")});   
    this.setState({xvalues: this.state.xvalues[0]});  
    this.setState({yvalues: this.state.yvalues[0]});  
    this.setState({obj: {x: this.state.xvalues, y: this.state.yvalues}})
    this.forceUpdate();
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
      <div class="wrapper container">
        <Table selectedObj={this.state.obj}/>
        <div class="row">
          <select class="rounded form-control-md col-lg-6" onChange={e => this.changeCategory(e.target)}>{optionItems}</select>
        </div>
      </div>
    )
  }
}

String.prototype.capitalize = function() {
  return this.charAt(0).toUpperCase() + this.slice(1);
}

export default App;
