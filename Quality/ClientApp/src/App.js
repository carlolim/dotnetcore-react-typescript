import React, { Component } from 'react';
import { Route, Switch, Redirect } from 'react-router';
import { hasToken } from './helpers/common-helpers';

//import components
import Dashboard from "./components/dashboard";
import Product from "./components/product";

import Login from "./components/user/login";

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={(props) => 
    ( hasToken() ? <Component {...props} /> : <Redirect to='/login' /> )} 
  />
)

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Switch>
        <Redirect exact path="/" to="/products" />
        <PrivateRoute exact path='/products' component={Product} />
        <Route exact path="/login" component={Login} />
      </Switch>
    );
  }
}
