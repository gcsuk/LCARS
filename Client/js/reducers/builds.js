function builds(state = [], action) {
    switch (action.type)
    {
        case 'REFRESH_BUILDS':
            return action.builds;
        default:
            return state;
    }
}

export default builds;