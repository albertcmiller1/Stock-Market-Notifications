import React, { Component } from "react";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input
} from "reactstrap";

import { Confirm } from "./Confirmation";

export class Build extends Component {
  constructor(props) {
    super(props);
    this.state = {
      addAlertClicked: true,
      Email: {
        Subject: "",
        Content: "",
        SendFrom: "",
        Admin: true
      }
    };
  }

  addEmail = (newEmail) => {
    this.props.addEmail(newEmail)
  }

  handleChange = e => {
    const {
      target: { name, value }
    } = e;

    this.setState(
      {
        Email: {
          ...this.state.Email,
          [name]: value
        }
      },
      () => {}
    );
  };

  checkBoxHandle = e => {
    const {
      target: { name }
    } = e;
    this.setState({
      Email: {
        ...this.state.Email,
        [name]: !this.state.Email[name]
      }
    }, () => { });
  };

  AddAlertBtn = () => {
    this.setState({
      addAlertClicked: !this.state.addAlertClicked
    });
  };

  render() {
    return (
      <div>
        {this.state.addAlertClicked ? (
          <div>
            <h2>Add an Email</h2>
            <Form>
              <FormGroup>
                <Label>Subject</Label>
                <Input
                  type="text"
                  placeholder="Enter"
                  value={this.state.Email.Subject}
                  name="Subject"
                  onChange={this.handleChange}
                />
              </FormGroup>

              <FormGroup>
                <Label for="exampleText">Content</Label>
                <Input
                  type="textarea"
                  name="Content"
                  onChange={this.handleChange}
                />
                </FormGroup>

                <FormGroup>
                    <Label>Who would you like this email to be sent from</Label>
                    <Input
                        type="text"
                        placeholder="example@gmail.com"
                        name="SendFrom"
                        onChange={this.handleChange}
                    />
                </FormGroup>

              <br />
              <Button
                outline
                size="lg"
                color="success"
                onClick={this.AddAlertBtn}
              >
                Add
              </Button>{' '}

              <Button outline size="lg" color="danger" onClick={this.props.back}>
                Back
              </Button>
              <br />
              <br />
            </Form>
          </div>
        ) : (
             <Confirm alert={this.state.Email} back={this.AddAlertBtn} cancel={this.props.back} addEmail={this.addEmail} />
        )}
      </div>
    );
  }
}
