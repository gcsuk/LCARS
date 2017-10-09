function deployments(state = [], action) {
    switch (action.type)
    {
        case 'REFRESH_DEPLOYMENTS':
            return action.deployments;
        default:
            return state;
    }
}

export default deployments;