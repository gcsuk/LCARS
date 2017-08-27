import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';

const Builds = styled.div`
  text-align: center;
`;

const Build = styled.div`
  margin: 10px 0
`;

const ProjectName = styled.span`
  display: inline-block;
  width: 200px;
  text-align: left
`;

const BuildStatus = styled.span`
  display: inline-block;
  padding: 3px 35px;
  font-size: 30px;
  border-radius: 20px;
  background-color: #015001
`;

class BuildsPage extends Component { 
  constructor(props) {
    super(props);
    
    const builds = new Array(5);

    for (var row = 0; row < 5; row++){
        builds[row] = {};
        builds[row].name = "Project " + row;
        builds[row].status = "SUCCESS";
        builds[row].version = row + ".0.0.0";
    }

    this.state = {builds: builds};
  }

  render() {
    return (
      <Builds>
        { this.state.builds.map((project, rowIndex) => (
          <Build>
            <ProjectName row={rowIndex}>
              {project.name}
            </ProjectName>
            <BuildStatus row={rowIndex}>
              {project.status} - {project.version}
            </BuildStatus>
          </Build>)
        )}
      </Builds>
    );
  }
}

export default BuildsPage;