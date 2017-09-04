export const refreshBuilds = builds => {
  return {
    type: 'REFRESH_BUILDS',
    builds
  }
}

export const refreshDeployments = deployments => {
  return {
    type: 'REFRESH_DEPLOYMENTS',
    deployments
  }
}

export const refreshEnvironments = environments => {
  return {
    type: 'REFRESH_ENVIRONMENTS',
    environments
  }
}