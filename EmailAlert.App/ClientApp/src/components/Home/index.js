import React from 'react';
//import Chart from './Charts/CandleStickChartForContinuousIntraDay';
import Chart from './Charts/CandleStickChartWithMA';
import { TypeChooser } from "react-stockcharts/lib/helper";

export class ChartComponent extends React.Component {
    render() {
        return (
            <TypeChooser>
                {type => <Chart type={type} data={this.props.Stocks} />}
            </TypeChooser>
        )
    }
}

