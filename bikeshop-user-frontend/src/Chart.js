import React, {Component} from 'react';
import {Bar, Line, Pie} from 'react-chartjs-2';
export default class Chart extends Component {
    constructor(props){
        super(props);
        this.state = {
            chartData: {
                labels: this.props.xVals,
                datasets:[
                    {
                        label: 'Saleprice',
                        data: this.props.yVals,
                        backgroundColor:this.props.backgroundColor
                    }
                ]
            }
        }
            
    }
    
    
    render() {
        return (
            <div className="chart">
                <Bar
                data={this.state.chartData} />

            </div>
        )
    }
}