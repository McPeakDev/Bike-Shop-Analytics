import React, { Component } from 'react'
import data from '../data'



class Comparison extends Component
{
    
    handleClick = (e) => {
        alert("the color of " + e.fruit + " is " + e.color)
    }
    render()
    {   
        return(
            <div>
               {data.Test.map((test, i) => 
               <button className="buttons" key={i} onClick={() => this.handleClick(test)}>{test.fruit}</button>)}
            </div>
        )
    }
}

export default Comparison;