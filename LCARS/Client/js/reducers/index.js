import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';

import builds from './builds';
import deployments from './deployments';
import environments from './environments';

const rootReducer = combineReducers({ builds, deployments, environments, routing: routerReducer});

export default rootReducer;