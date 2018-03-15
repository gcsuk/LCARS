import { SET_ALERT } from '../actions';

function alertCondition(state = [], action) {
  switch (action.type) {
    case SET_ALERT:
      return action.alertCondition;
    default:
      return state;
  }
}

export default alertCondition;