function git(state = [], action) {
    switch (action.type)
    {
        case 'REFRESH_GIT':
            return action.git;
        default:
            return state;
    }
}

export default git;