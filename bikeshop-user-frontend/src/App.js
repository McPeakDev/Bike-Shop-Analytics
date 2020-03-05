import React, {Component} from "react"
import logo from "./logo.svg";
import "./App.css"
import Api from './APIClient'

class App extends Component {
  constructor(props)
  {
    super(props)
  }
 
  render(){
    return(
      <Api></Api>
    )
  }
}

export default App;
