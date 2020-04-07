import React, {Component} from 'react';
import {Line, defaults} from 'react-chartjs-2';

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
                        borderColor: 'rgb(204,1204,0)',
                        fill: false
                    }
                ]
            }
        }
        defaults.global.defaultFontColor = "#ffffff";
            
    }
    
    render() {
        return (
            <div className="bg-dark text-white">
                <Line data={this.state.chartData} dark/>
            </div>
        )
    }
}