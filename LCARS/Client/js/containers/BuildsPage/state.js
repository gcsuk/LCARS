// @flow
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../actions';

export function mapStateToProps(state: Object) {
  return {
      builds: state.builds
  };
}

export function mapDispatchToProps(dispatch: Dispatch) {
  return bindActionCreators(actionCreators, dispatch);
}