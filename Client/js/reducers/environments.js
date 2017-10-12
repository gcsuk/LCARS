function environments(state = [], action) {
    switch (action.type)
    {
        case 'REFRESH_ENVIRONMENTS':
            return action.environments;
        default:
            return state;
    }
}

export default environments;