import React, {Component} from "react"
import logo from "./logo.svg";
import "./App.css"
import Table from "./Table"
import API from './APIClient'


class App extends Component {
  constructor(props)
  {
    super(props)
    this.state = {data: [], obj: {}};
    this.change = this.change.bind(this);
  }
  async componentDidMount() 
  {
      let api = new API();
      this.setState({ data: await api.get("bike", "readall")});   
      this.setState(() => ({obj: this.state.data[0]}));   
      console.log(this.state.obj);

  }

  change(event) 
  {
    console.log(event.value)
    this.setState(() => ({obj: this.state.data[event.value]}));
    this.forceUpdate();
  }

  render(){
    let i = 0;
    let optionItems = this.state.data.map((item) => 
            <option value={i++} key={item.bikeID} >{item.name}</option>
        );
    return(
      <div>
        <select ref="bikes" onChange={e => this.change(e.target)}>{optionItems}</select>
        <Table selectedObj={this.state.obj}/>
      </div>
    )
  }
}

export default App;
