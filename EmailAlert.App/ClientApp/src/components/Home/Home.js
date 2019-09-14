import React, { Component } from 'react';
import { FormGroup, Label, Input, Container, Row, Col, Button } from 'reactstrap';
import { ChartComponent } from "./index";
import { timeParse } from "d3-time-format";

//const parseDate = timeParse("%Y-%m-%dT%H:%M:%S");
const parseDate = timeParse("%Y-%m-%dT%H:%M:%S");

export class Home extends Component {
  static displayName = Home.name;
    constructor(props) {
        super(props)

        this.state = {
            ticker: "MSFT",
            intradayFrequency: "5min",
            amountOfData: "full",
            timeSeries: "TIME_SERIES_INTRADAY",
            description: "Time Series (5min)",
            stocks: []
        }
    }

    componentDidMount() {
        window.setInterval(this.getStocks(), 60000)
    }

    getStocks() {
        const PostURL = "api/UIcontroller/UpdateStock";
        this.buildDescription();

        fetch(PostURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Ticker: this.state.ticker,
                IntradayFrequency: this.state.intradayFrequency,
                AmountOFData: this.state.amountOfData,
                TimeSeries: this.state.timeSeries,
                Description: this.state.description
            })
        }).then(r => {
            return r.json();
        }).then(p => {
            var data = this.parseData(p)

            data.sort((a, b) => {
                return a.date.valueOf() - b.date.valueOf();
            })

            this.setState({
                stocks: data
            })
        }).catch(e => alert("The Stock You're looking for is unavaliable. Try entering different information"));
    }

    handleClick = (e) => {
        const {
            target: { name, selectedOptions }
        } = e;

        this.setState({
            [name]: selectedOptions[0].getAttribute('name')
        }, () => { });
    }

    chooseStock = (e) => {
        this.setState({
            ticker: e.target.value
        }, () => { });
    }

    buildDescription = () => {
        switch (this.state.timeSeries) {
            case "TIME_SERIES_INTRADAY":
                this.setState({ description: "Time Series (" + this.state.intradayFrequency + ")" })
                break
            case "TIME_SERIES_DAILY":
                this.setState({ description: "Time Series (Daily)" })
                break;
            case "TIME_SERIES_DAILY_ADJUSTED":
                this.setState({ description: "Time Series (Daily)" })
                break;
            case "TIME_SERIES_WEEKLY":
                this.setState({ description: "Weekly Time Series" })
                break;
            case "TIME_SERIES_WEEKLY_ADJUSTED":
                this.setState({ description: "Weekly Adjusted Time Series" })
                break;
            case "TIME_SERIES_MONTHLY":
                this.setState({ description: "Monthly Time Series" })
                break;
            case "TIME_SERIES_MONTHLY_ADJUSTED":
                this.setState({ description: "Monthly Adjusted Time Series" })
                break;
            default: 
                break
        }
    }
    
    parseData = array => {
        var newArray = []

        for (var i = 0; i < array.length; i++) {

            var tempStock = Object.assign({}, array[i])
            tempStock.date = parseDate(array[i].date)
            newArray.push(tempStock)
        }

        return newArray
    }



  render () {
    return (
      <div>
        <h1>Welcome Home!</h1>
            <Container>
                <Row>
                    <Col>
                        <Label for="exampleSelect">Time Series</Label>
                        <Input type="select" name="timeSeries" onClick={(e) => this.handleClick(e)}>
                            <option name="TIME_SERIES_INTRADAY">Intraday</option>
                            <option name="TIME_SERIES_DAILY">Daily</option>
                            <option name="TIME_SERIES_DAILY_ADJUSTED">Daily Adjusted</option>
                            <option name="TIME_SERIES_WEEKLY">Weekly</option>
                            <option name="TIME_SERIES_WEEKLY_ADJUSTED">Weekly Adjusted</option>
                            <option name="TIME_SERIES_MONTHLY">Monthly</option>
                            <option name="TIME_SERIES_MONTHLY_ADJUSTED">Monthly Adjusted</option>
                        </Input>
                    </Col>

                    <Col>
                        <Label for="exampleSelect">Frequency</Label>
                        <Input type="select" name="intradayFrequency" onClick={(e) => this.handleClick(e)}>
                            <option name="1min">One Minute</option>
                            <option name="5min">Five Minutes</option>
                            <option name="15min">Fifteen Minutes</option>
                            <option name="30min">Thirty Minutes</option>
                            <option name="60min">One Hour</option>
                        </Input>
                    </Col>

                    <Col>
                        <Label for="exampleSelect">Data Size</Label>
                        <Input type="select" name="amountOfData" onClick={(e) => this.handleClick(e)}>
                            <option name="full">All Avaliable</option>
                            <option name="compact">Most Recent 100</option>
                        </Input>
                    </Col>

                    <Col>
                        <FormGroup>
                            <Label>Choose a Stock</Label>
                            <Input placeholder="TSLA" onChange={e => { this.chooseStock(e) }} />
                        </FormGroup>
                    </Col>

                    <Col>
                        <Label for="exampleSelect" className="d-block">Modify</Label>
                        <Button outline color="primary" onClick={() => this.getStocks()}>Update</Button>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <b>Current Stock:</b> {this.state.ticker}
                    </Col>
                    <Col>
                       <b>Currently Viewing:</b> {this.state.description}
                    </Col>
                    <Col>
                    </Col>
                </Row>
            </Container>
            <br />
            {this.state.stocks.length ? <ChartComponent Stocks={this.state.stocks} /> :
                <div>
                    <h2>Working...</h2>
                    <div>if graph has not loaded after several seconds, the stock data you entered may not be availiable.</div>
                    <div>Try clicking update again, entering new information, or a different stock.</div>
                </div>
            }
        </div>
      );
  }
}
