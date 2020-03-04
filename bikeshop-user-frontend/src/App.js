import React, {Component} from "react"
import logo from "./logo.svg";
import "./App.css"
import Comparisons from './Components/Comparisons'

class App extends Component {
  constructor(props)
  {
    super(props)
  }
  render(){
    return(
      <div>
        <Comparisons></Comparisons>
      </div>
      
    )
  }
}

export default App;
