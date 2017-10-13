import 'babel-polyfill';
import React from 'react';
import { render } from 'react-dom';
import { ConnectedRouter } from 'react-router-redux';
import Layout from './js/containers/Layout';
import './css/index.css';
import { Provider } from 'react-redux'
import { createStore } from 'redux'
import store, { history } from './js/store';

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <Layout/>
    </ConnectedRouter>
  </Provider>,
  document.getElementById('app')
);
