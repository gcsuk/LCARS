import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import gitData from '../../data/git';

function isShipped(element) {
  
}

const Git = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle;
  font-size: 48pt
`;

class GitPage extends Component {
  constructor(props) {
    super(props);

    this.state = {git: gitData};
  }

  render() {
    return (
      <Git>
        <thead>
          <tr>
            <td></td>
            <td>Pending</td>
            <td>Shipped</td>
            <td>Branches</td>
          </tr>
        </thead>
        <tbody>
          { this.state.git.map((repo, rowIndex) => (
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

export default GitPage;
