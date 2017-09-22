export const REFRESH_BUILDS = 'REFRESH_BUILDS';
export const REFRESH_DEPLOYMENTS = 'REFRESH_DEPLOYMENTS';
export const REFRESH_ENVIRONMENTS = 'REFRESH_ENVIRONMENTS';

export const refreshBuilds = builds => {
  return {
    type: REFRESH_BUILDS,
    builds
  }
}

export const refreshDeployments = deployments => {
  return {
    type: REFRESH_DEPLOYMENTS,
    deployments
  }
}

export const refreshEnvironments = environments => {
  return {
    type: REFRESH_ENVIRONMENTS,
    environments
  }
}