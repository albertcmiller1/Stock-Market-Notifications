import React, { Component } from "react";
import { Button, Col, Row, Container } from 'reactstrap';

export class SendMail extends Component {
    constructor(props) {
        super(props)
    }

    send = (ID) => {
        const SendUrl = "api/UIcontroller/SendEmail"

        fetch(SendUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: ID
            })
        .then(r => {
            return r.text();
        }).then(p => {
            console.log("response", p)
            alert("Your Email has been sent!")
        }).catch(e => alert(e));
    }


    delete = (ID) => {
        const deleteUrl = "api/UIcontroller/DeleteEmail"
        //var EmailToSend = this.props.emails.filter(email => email.id === ID)[0]

        fetch(deleteUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: ID
        })
        .then(r => {
            return r.json();
        }).then((p) => {
            console.log(p);
            this.props.EmailAdded(p)
        }).catch(e => alert(e));
    }

    render() {
        return (
            <div>
                <h1>Send Mail</h1>
                <br />

                <Container>
                    <Row>
                        <Col xs="2"><b>Subject</b></Col>
                        <Col md={7}><b>Content</b></Col>
                        <Col xs="1"><b></b></Col>
                        <Col xs="1"><b></b></Col>
                    </Row>
                    <br />
                    {this.props.emails.map(m =>
                        <div key={m.id}>
                            <Row >
                                <Col xs="12" md={2}>{m.subject}</Col>
                                <Col md={7} xs="12">{m.content}</Col>
                                <Col xs="6" md={1}><Button
                                    outline color="primary"
                                    onClick={ () => this.send(m.id)}>
                                    Send
                                    </Button>
                                </Col>
                                <Col xs="6" md={1}>
                                    <Button
                                        outline color="danger"
                                        onClick={() => this.delete(m.id)}>
                                        Remove
                                    </Button>
                                </Col>
                            </Row>
                            <br />
                        </div>
                    )}
                </Container>
               
                <Button outline size="lg" color="danger" onClick={this.props.back}>
                    Back
                </Button>
                <br />
                <br />
            </div>
            )
    }
}
