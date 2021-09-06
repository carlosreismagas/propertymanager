import React, { useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';

import './custom.css'
import Login from './components/Login/Login';
import { ListProperty } from './components/Property/ListProperty';
import { EditProperty } from './components/Property/EditProperty';

function App() {
  var session = localStorage.getItem("user_session");
  if(session) {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/property' component={ListProperty} />
        <Route exact path='/property/:id' component={EditProperty} />
      </Layout>
    );
  } else {
    return (
      <Login />
    );
  }
}

export default App;