export function fetchPosts(){
  const request = axios.get(`${ROOT_URL}/posts${API_KEY}`);

  return {
    type: 'FETCH_POSTS',
    payload: request
  };
}


export function createPost(props){
  const request = axios.post(`${ROOT_URL}/posts${API_KEY}`, props);

  return{
    type:'CREATE_POST',
    payload:request
  };
}
