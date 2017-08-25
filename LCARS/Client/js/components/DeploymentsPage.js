import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';

const Deployments = styled.div`
  display: grid;
  grid-template-rows: repeat(${props => (props.rowCount)}, 100px);
  grid-template-columns: repeat(${props => (props.colCount)}, 100px); 
`;

const ProjectName = styled.div`
  grid-row: ${props => (props.rowIndex + 1)} / ${props => (props.rowIndex + 2)};
  border: solid 1px #fff
`;

const ProjectDeploy = styled.div`
  grid-row: ${props => (props.rowIndex + 1)} / ${props => (props.rowIndex + 2)};
  grid-column: ${props => (props.colIndex + 2)} / ${props => (props.colIndex + 3)};
  border: solid 1px #fff
`;

class DeploymentsPage extends Component { 
  constructor(props) {
    super(props);
    
    const deployments = new Array(5);

    for (var row = 0; row < 5; row++){
        deployments[row] = {};
        deployments[row].name = "Zeus " + row;
        deployments[row].deploys = new Array(5);
        for(var col = 0; col < 5; col++){ 
            deployments[row].deploys[col] = row + '.' + col;
        }
    }

    this.state = {deployments: deployments};
  }

  render() {
    return (
      <Deployments rowCount={this.state.deployments.length} colCount={this.state.deployments[0].deploys.length}>
        { this.state.deployments.map((project, rowIndex) => (
          <div key={rowIndex} rowIndex={rowIndex}>
            <ProjectName rowIndex={rowIndex}>
              {project.name}
            </ProjectName>
            { project.deploys.map((item, colIndex) => (
              <ProjectDeploy key={colIndex} rowIndex={rowIndex} colIndex={colIndex} className="project-deploy">
                {item}
              </ProjectDeploy>
            ))}
          </div>)
        )}
      </Deployments>
    );
  }
}

export default DeploymentsPage;
