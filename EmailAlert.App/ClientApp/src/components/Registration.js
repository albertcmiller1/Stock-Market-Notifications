import React, { Component } from 'react';
import { Col, Row, Button, Form, FormGroup, Label, Input, InputGroup, InputGroupAddon } from 'reactstrap';

export class Registration extends Component {

  constructor (props) {
      super(props);
      this.state = {
          UIFirstName: '',
          LastName: '',
          UserName: '',
          Email: '',
          checks: {
              UpFive: false,
              DownFive: false,
              MovingAvg: false,
              Admin: false
          },
          Stocks: [],
          tempStock: ""
      }
  }
    
    addUser = () => {
        const PostURL = "api/UIcontroller/RegisterUser";
        fetch(PostURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Id: null,
                FirstName: this.state.UIFirstName,
                LastName: this.state.LastName,
                Username: this.state.UserName,
                Email: this.state.Email,
                Stocks: this.state.Stocks.map(p => { return { stock: p };}),
                UpFive: this.state.checks.UpFive,
                DownFive: this.state.checks.DownFive,
                MovingAvg: this.state.checks.MovingAvg,
                Admin: this.state.checks.Admin
            })
        }).then(r => {
            return r.json();
        }).then(p => {
            this.props.UserAdded(p)
            alert("Congratulations! You're Registered.")
            this.props.history.push('/');
        }).catch(e => alert("Something went wrong with your registration: ", e));
    }

    handleChange = e => {
        const {
            target: { name, value }
        } = e;

        this.setState(
            {
                [name]: value
            }, () => {}
        )
    };

    checkBoxHandle = e => {
        const {
            target: { name }
        } = e;

        this.setState({
            checks: {
                ...this.state.checks,
                [name]: !this.state.checks[name]
            }
        }, () => { });
    };

    temp = (e) => {
        this.setState({
            tempStock: e.target.value
        }, () => { })
    }

    addStock = () => {
        
        var stock = this.state.tempStock
        var newList = this.state.Stocks;
        newList.push(stock)

        this.setState({
            Stocks: newList,
            tempStock: ""
        }, () => { })
    }

    goBack = () => {
        this.props.history.push('/');
    }

  render () {
    return (
        <div>
            <br />
            <h2>Sign up to Receive Notifications About Your Stocks.</h2>
            <br />
            <Form>
                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label>First Name</Label>
                            <Input placeholder="John" name="UIFirstName" onChange={this.handleChange}/>
                            </FormGroup>
                        </Col>
                        <Col md={6}>
                            <FormGroup>
                                <Label>Last Name</Label>
                            <Input placeholder="Doe" name="LastName" onChange={this.handleChange}/>
                            </FormGroup>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label>Username</Label>
                            <Input placeholder="JohnDoe1" name="UserName" onChange={this.handleChange}/>
                            </FormGroup>
                        </Col>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="exampleEmail">Email</Label>
                            <Input type="email" name="Email" onChange={this.handleChange} placeholder="JohnDoe@gmail.com" />
                            </FormGroup>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={6}>
                            
                        <Label>Enter any stocks you would like to recieve notifications about</Label>
                        <InputGroup>
                            <Input placeholder="MSFT" value={this.state.tempStock} onChange={this.temp}/>
                            <InputGroupAddon addonType="append">
                                <Button color="secondary" onClick={this.addStock}>Add</Button>
                            </InputGroupAddon>
                        </InputGroup>


                        </Col>
                        <Col md={6}>
                        {this.state.Stocks.map(s =>
                            <div key={s}>{s}</div>
                                )}
                        </Col>
                    </Row>

                <br />

                <FormGroup check>
                    <Input type="checkbox"  onChange={this.checkBoxHandle} name="UpFive"/>
                    <Label for="StockMarket" check>Recieve an Email if your Stock is up 5% for the Day</Label>
                </FormGroup>

                <FormGroup check>
                    <Input type="checkbox"  onChange={this.checkBoxHandle} name="DownFive"/>
                    <Label check>Recieve an Email if your Stock is down 5% for the Day</Label>
                </FormGroup>

                <FormGroup check>
                    <Input type="checkbox" value={this.state.StockMarketGraph} onChange={this.checkBoxHandle} name="MovingAvg" />
                    <Label check>Recieve an Email When your Stock's Moving Average Crosses Market Price</Label>
                </FormGroup>

                <FormGroup check>
                    <Input type="checkbox" value={this.state.Admin} onChange={this.checkBoxHandle} name="Admin"/>
                    <Label check>Recieve Administrative Notifications About Our Market Insights</Label>
                </FormGroup>

                <br />
                <Button outline size="lg" color="success" onClick={this.addUser}>Submit</Button>{' '}
                <Button outline size="lg" color="danger" onClick={this.goBack}>Cancel</Button>{' '}

            </Form>
      </div>
    );
  }
}
