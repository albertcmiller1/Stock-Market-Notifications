import React, { Component } from "react";
import { Table, Button } from 'reactstrap';

export class Subscribers extends Component {

    deleteUser = (id) => {
        const PostURL = "api/UIcontroller/DeleteUser";


        fetch(PostURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: id 
        })
        .then(r => {
            console.log(r)
            return r.json();
        }).then(p => {
            this.props.UserDeleted(p)
            console.log(p)
        })
    }

    


    render() {
        return (
            <div>
                 <Table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Delete</th>
                        </tr>
                    </thead>

                    <tbody>
                        {this.props.Users.map(m =>
                            <tr key={m.id}>
                                <th scope="row">{m.id}</th>
                                <td>{m.firstName}</td>
                                <td>{m.lastName}</td>
                                <td>{m.email}</td>
                                <td><Button
                                    outline color="primary"
                                    onClick={() => this.deleteUser(m.id)}>
                                    Remove
                                    </Button></td>
                            </tr>
                        )}
                    </tbody>
                </Table>

                <Button outline size="lg" color="danger" onClick={this.props.back}>
                    Back
                </Button>
            </div>
    )}
}
