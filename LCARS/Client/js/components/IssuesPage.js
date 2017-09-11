import React from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import issueData from '../../data/issues';

const Issues = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle
`;

class IssuesPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {issues: issueData};
    console.log(this.state.issues[0]);
  }
  render() {
    return (
      <Issues>
        <thead>
          <tr>
            <th>Priority</th>
            <th>#</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          { this.state.issues.map((issue, rowIndex) => (
            <tr>
                <td className="issueId">{issue.id}</td>
                <td>{issue.summary}</td>
                <td className="priority"><img src={issue.priorityIcon} alt={issue.priority} /></td>
            </tr>
          ))}
        </tbody>
      </Issues>
    );
  }
}

export default IssuesPage;
