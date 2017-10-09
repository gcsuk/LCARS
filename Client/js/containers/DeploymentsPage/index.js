import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import {connect} from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';

const Deployments = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle
`;

const ProjectName = styled.td`
  width: 150px;
  text-align: left
`;

const ProjectDeploy = styled.div`
  display: block;
  height: 40px;
  margin: 5px;
  font-size: 1.8rem;
  border-radius: 20px;
  background-color: #015001
`;

class DeploymentsPage extends Component { 
  render() {
    return (
      <Deployments>
        <thead>
          <tr>
            <th></th>
            { this.props.deployments[0].deploys.map((item, colIndex) => (
              <th key={colIndex}>
                {item.env}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          { this.props.deployments.map((project, rowIndex) => (
            <tr key={rowIndex}>
              <ProjectName>
                {project.name}
              </ProjectName>
              { project.deploys.map((item, colIndex) => (
                <td key={colIndex}>
                  <ProjectDeploy>{item.version}</ProjectDeploy>
                </td>
              ))}
            </tr>)
          )}
        </tbody>
      </Deployments>
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(DeploymentsPage);