import React, { useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';

import './custom.css'
import Login from './components/Login/Login';

const App = () => {
  var session = localStorage.getItem("user_session");
  if(session) {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
      </Layout>
    );
  } else {
    return (
      <Login />
    );
  }
}

export default App;