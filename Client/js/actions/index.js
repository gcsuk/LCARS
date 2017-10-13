export const REFRESH_BUILDS = 'REFRESH_BUILDS';
export const REFRESH_DEPLOYMENTS = 'REFRESH_DEPLOYMENTS';
export const REFRESH_ENVIRONMENTS = 'REFRESH_ENVIRONMENTS';
export const REFRESH_GIT = 'REFRESH_GIT';
export const UPDATE_CONFIG = 'UPDATE_CONFIG';

export const refreshBuilds = builds => {
  return {
    type: REFRESH_BUILDS,
    builds
  };
};

export const refreshDeployments = deployments => {
  return {
    type: REFRESH_DEPLOYMENTS,
    deployments
  };
};

export const refreshEnvironments = environments => {
  return {
    type: REFRESH_ENVIRONMENTS,
    environments
  };
};

export const refreshGit = git => {
  return {
    type: REFRESH_GIT,
    git
  };
};

export const updateConfig = config => ({
    type: UPDATE_CONFIG,
    config
});