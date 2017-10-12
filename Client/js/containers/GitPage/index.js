import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';

function isShipped(element) {
  
}

const Git = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle;
  font-size: 48pt
`;

class GitPage extends Component {
  render() {
    return (
      <Git>
        <thead>
          <tr>
            <td />
            <td>Pending</td>
            <td>Shipped</td>
            <td>Branches</td>
          </tr>
        </thead>
        <tbody>
          { this.props.git.map((repo, rowIndex) => (
            <tr key={rowIndex}>
              <td>
                {repo.repository}
              </td>
              <td>
                {repo.pullRequests.length}
              </td>
              <td>
                {repo.pullRequests.filter((e) => e.isShipped).length}
              </td>
              <td>
                {repo.branches.length}
              </td>
            </tr>
          ))}
        </tbody>
      </Git>
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(GitPage);