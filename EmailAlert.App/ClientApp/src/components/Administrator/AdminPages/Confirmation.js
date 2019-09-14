import React, { Component } from "react";
import {
  Button,
  ListGroup,
  ListGroupItem,
  ListGroupItemHeading,
  ListGroupItemText
} from "reactstrap";

export class Confirm extends Component {

    

    postData() {
        const PostURL = "api/UIcontroller/CreateEmail";

        fetch(PostURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Subject: this.props.alert.Subject,
                Content: this.props.alert.Content,
                From: this.props.alert.SendFrom,
                AdminNote: true
            })
        }).then(r => {
            return r.json();
        }).then(p => {
            this.props.addEmail(p);
            alert("you added an email!");
            this.props.cancel();
        }).catch(e => alert(e));
    }

  render() {
    return (
      <div>
        <h2>Confirm Information</h2>

        <ListGroup>
          <ListGroupItem>
            <ListGroupItemHeading>Subject</ListGroupItemHeading>
            <ListGroupItemText>{this.props.alert.Subject}</ListGroupItemText>
          </ListGroupItem>



       
          <ListGroupItem>
            <ListGroupItemHeading>Content</ListGroupItemHeading>
            <ListGroupItemText>{this.props.alert.Content}</ListGroupItemText>
          </ListGroupItem>

            <ListGroupItem>
                <ListGroupItemHeading>From Email Address</ListGroupItemHeading>
                <ListGroupItemText>{this.props.alert.SendFrom}</ListGroupItemText>
            </ListGroupItem>
         

          
        </ListGroup>
        <br />
        <Button outline size="lg" color="success" onClick={()=>this.postData()}>
          Confirm
        </Button>{' '}
        <Button outline size="lg" color="danger" onClick={this.props.back}>
          Back
        </Button>{' '}
        <Button outline size="lg" color="danger" onClick={this.props.cancel}>
                Cancel
        </Button>
        <br />
        <br />
        <br />
      </div>
    );
  }
}
