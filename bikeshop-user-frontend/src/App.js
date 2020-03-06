import React, {Component} from "react"
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Table from "./Table"
import API from './APIClient'


class App extends Component {
  constructor(props) 
  {
    super(props)
    this.state = {data: [], obj: {}};
    this.changeData = this.changeData.bind(this);
    this.changeTable = this.changeTable.bind(this);

  }
  async componentDidMount() 
  {
      let api = new API();
      this.setState({ data: await api.get("bike", "readall")});   
      this.setState({obj: this.state.data[0]});  
      console.log(this.state.obj);

  }

  changeData(event) 
  {
    this.setState(() => ({obj: this.state.data[event.value]}));
    this.forceUpdate();
  }

  async changeTable(event) 
  {
    let api = new API();
    this.setState({ data: await api.get(event.value, "readall")});   
    this.setState({obj: this.state.data[0]});  
    this.forceUpdate();
  }

  getKeys() 
  {
      return Object.keys(this.state.data);
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
    return(
      <div class="wrapper container">
        <Table selectedObj={this.state.obj}/>
        <div class="row">
          <select class="rounded form-control-md col-lg-6" ref="bikes" onChange={e => this.changeTable(e.target)}>
            <option selected value="bike">Bikes</option>
            <option value="category">Categorys</option>
            <option value="admin">Admins</option>
          </select>
          <select class="rounded form-control-md col-lg-6" ref="bikes" onChange={e => this.changeData(e.target)}>{optionItems}</select>
        </div>
      </div>
    )
  }
}

String.prototype.capitalize = function() {
  return this.charAt(0).toUpperCase() + this.slice(1);
}

export default App;
