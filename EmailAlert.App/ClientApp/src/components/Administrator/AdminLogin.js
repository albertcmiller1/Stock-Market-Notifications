import React, { Component } from "react";
import { Col, Row, Button, Form, FormGroup, Label, Input } from "reactstrap";
import { AdminHome } from "./AdminHome";

export class Admin extends Component {
  constructor(props) {
    super(props);

    this.state = {
      adminClicked: true,
      UIEmail: "",
      UIPassword: ""
    };
  }

  postLogInData() {
      const PostURL = "api/UIcontroller/Authorize";

    var getDataPromise = new Promise((res, rej) => {
      fetch(PostURL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          Email: this.state.UIEmail,
          Password: this.state.UIPassword
        })
      })
        .then(r => r.json())
        .then(res)
        .catch(rej);
    });
    return getDataPromise;
  }

  AdminBtn = () => {
    this.postLogInData()
      .then(login => {
        if (login === true) {
          this.setState({
            adminClicked: !this.state.adminClicked
          });
        } else {
          this.setState({
            adminClicked: this.state.adminClicked
          });
        }
      })
      .catch(e => {
        alert(e);
      });
  };

  //handleChange = e => {
  //    const {
  //        target: { name, value }
  //    } = e;

  //    this.setState(
  //        {
  //            Email: {
  //                ...this.state.Email,
  //                [name]: value
  //            }
  //        },
  //        () => { }
  //    );
  //};

  handleChange1 = e => {
    this.setState({
      UIEmail: e.target.value
    });
  };

  handleChange2 = e => {
    this.setState({
      UIPassword: e.target.value
    });
  };

  render() {
    return (
      <div>
        {this.state.adminClicked ? (
            <div>
            <br />
            <h2>Please Sign in to Access The Administration Page</h2>
            <br />
            <Form>
              <Row>
                <Col md={6}>
                  <FormGroup>
                    <Label for="exampleEmail">Email</Label>
                    <Input
                      type="email"
                      name="UIEmail"
                      placeholder="example@gmail.com"
                      onChange={this.handleChange1}
                    />
                  </FormGroup>
                </Col>
                <Col md={6}>
                  <FormGroup>
                    <Label for="examplePassword">Password</Label>
                    <Input
                      type="password"
                      name="UIPassword"
                      placeholder="password123"
                      onChange={this.handleChange2}
                    />
                  </FormGroup>
                </Col>
              </Row>
              <br />
            <Button outline size="lg" color="success" onClick={this.AdminBtn} >
                Submit
              </Button>
            </Form>
          </div>
        ) : (
                    <AdminHome Users={this.props.Users} UserDeleted={this.props.UserDeleted}/>
        )}
      </div>
    );
  }
}
