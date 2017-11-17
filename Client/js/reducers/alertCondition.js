import { SET_ALERT } from '../actions';

function alertCondition(state = [], action) {
  switch (action.type) {
    case SET_ALERT: {
      return Object.assign({}, state, { condition: action.condition });
    }
  }

  return state;
}

export default alertCondition;