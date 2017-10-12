import { UPDATE_CONFIG } from '../actions';
const defaultState = {
  routeChangeInterval: 30000
};

function config(state = defaultState, action) {
  switch(action) {
    case UPDATE_CONFIG: {
      return Object.assign({}, state, action.config);
    }
    default: return state;
  }
}

export default config;