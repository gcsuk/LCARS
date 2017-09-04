import 'babel-polyfill';
import React from 'react';
import { render } from 'react-dom';
import { Router } from 'react-router';
import routes from './js/routes';
import './css/index.css';
import { Provider } from 'react-redux'
import { createStore } from 'redux'
import store, { history } from './js/store';

render(
  <Provider store={store}>
    <Router history={history} routes={routes} />
  </Provider>,
  document.getElementById('app')
);
