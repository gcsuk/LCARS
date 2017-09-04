import { createStore, compose } from 'redux';
import { syncHistoryWithStore } from 'react-router-redux';
import { browserHistory } from 'react-router';

import rootReducer from './reducers/index';

import deployments from '../data/deployments';
import builds from '../data/builds';
import environments from '../data/environments';

const defaultState = {
    deployments,
    builds,
    environments
};

const store = createStore(rootReducer, defaultState);

export const history = syncHistoryWithStore(browserHistory, store);

export default store;