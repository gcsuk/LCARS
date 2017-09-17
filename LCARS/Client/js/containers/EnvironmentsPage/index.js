import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';

const Environments = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle
`;

const Environment = styled.tr`
  border-bottom: solid 1px #fff;
  font-size: 36px
`;

const SiteName = styled.td`
  width: 150px;
  text-align: left
`;

class EnvironmentsPage extends Component { 
  render() {
    return (
      <Environments>
        <thead>
          <tr>
            <th></th>
            { this.props.environments[0].envs.map((item, colIndex) => (
              <th key={colIndex}>
                {item.name}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          { this.props.environments.map((site, rowIndex) => (
            <Environment key={rowIndex}>
              <SiteName>
                {site.name}
              </SiteName>
              { site.envs.map((item, colIndex) => (
                <td key={colIndex}>
                  {item.status}
                </td>
              ))}
            </Environment>)
          )}
        </tbody>
      </Environments>
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EnvironmentsPage);