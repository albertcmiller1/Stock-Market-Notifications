import React, { Component } from "react";
import { Build } from "./AdminPages/BuildEmail";
import { Subscribers } from "./AdminPages/Subscribers";
import { SendMail } from "./AdminPages/SendMail";
import { Card, Button, CardTitle, CardText, Row, Col } from 'reactstrap';

export class AdminHome extends Component {
    constructor(props) {
        super(props);

        this.state = {
            InputInfoPage: false,
            ViewUsersPage: false,
            SendMailPage: false,
            backBtn: false,
            Emails: []
        }


        const url2 = "api/UIcontroller/Emails"
        fetch(url2)
            .then(p => p.json())
            .then(q => {
                this.setState({ Emails: q }, () => { })
            });
    }

    choosePage = e => {
        const {
            target: { name }
        } = e;

        this.setState({
            [name]: !this.state[name]
        }, () => { });
    }

    changeView = (view) => {
        this.setState({
            [view] : !this.state[view]
        })
    }

    EmailAdded = (newEmail) => {
        this.setState({
            Emails: newEmail
        })
    }

    


    render() {

        if (this.state.InputInfoPage) {
            return (<Build back={() => this.changeView('InputInfoPage')} addEmail={this.EmailAdded} />);
        } else if (this.state.ViewUsersPage) {
            return (<Subscribers Users={this.props.Users} UserDeleted={this.props.UserDeleted} back={() => this.changeView('ViewUsersPage')} />)
        } else if (this.state.SendMailPage) {
            return (<SendMail back={() => this.changeView('SendMailPage')} Users={this.props.Users} emails={this.state.Emails} EmailAdded={this.EmailAdded}/>)
        }
        else if (!this.state.InputInfoPage && !this.state.ViewUsersPage) {
            return (
                <div>
                    <h1>Welcome, Administrator!</h1>
                        <Row>
                            <Col className="mt-3" sm="6">
                                <Card body>
                                    <CardTitle>Build An Email</CardTitle>
                                    <CardText>Set up your email and review.</CardText>
                                <Button outline color="primary" onClick={() => this.changeView('InputInfoPage')}>Build</Button>
                                </Card>
                            </Col>

                            <Col className="mt-3" sm="6">
                                <Card body>
                                    <CardTitle>Send Emails</CardTitle>
                                    <CardText>View all your created emails and send them.</CardText>
                                    <Button outline color="primary" name="SendMailPage" onClick={this.choosePage}>Send</Button>
                                </Card>
                            </Col>

                            <Col className="mt-3" sm="6">
                                <Card body>
                                    <CardTitle>View All Subscribers</CardTitle>
                                    <CardText>View all the people that are receieving your emails.</CardText>
                                    <Button outline color="primary" name="ViewUsersPage" onClick={this.choosePage}>View</Button>
                                </Card>
                            </Col>
                        </Row>
                </div>)
        }
    }
}

