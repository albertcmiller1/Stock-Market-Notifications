import React, { Component } from 'react';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';
import { Registration } from './components/Registration';
import { Admin } from './components/Administrator/AdminLogin';

export default class App extends Component {
  static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            Subscribers: []
        }
        const url = "api/UIcontroller/Users"
        fetch(url)
            .then(p => p.json())
            .then(q => {
                this.setState({ Subscribers: q }, () => { })
            }).catch(e => alert(e));
    }


    UpdateUsers = newUsers => {
        this.setState({
            Subscribers: newUsers
        })
    }
 

  render () {
    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
      return (
          <BrowserRouter basename={baseUrl}>
              <Layout>
                <Route exact path='/' component={Home} />

                <Route 
                    path="/admin"
                    render={routeProps => (
                        <Admin {...routeProps} Users={this.state.Subscribers} UserDeleted={this.UpdateUsers}/>
                    )}/>

                <Route
                    path="/registration"
                    render={routeProps => (
                        <Registration {...routeProps} UserAdded={this.UpdateUsers} />
                    )} />
              </Layout>
          </BrowserRouter>
    );
  }
}
