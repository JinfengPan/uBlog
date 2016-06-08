import React, {Component, PropTypes} from "react";
import {Link} from 'react-router';
import {reduxForm} from 'redux-form';
import {createPost} from '../../actions/postAction';

class PostsNew extends Component{
  render(){
    const {fields:{title, categories, content}, handleSubmit} = this.props;
    console.log(title);

    return(
      <form onsubmit={handleSubmit(this.props.createPost)}>
        <h3>Create A New Post</h3>

        <div className={`form-group ${title.touched && title.invalid ? 'has-danger' : ''}`}>
          <label>Title</label>
          <input type="text" className="form-control" {...title}/>
          <div className="text-help">
            {title.touched ? title.error : ''}
          </div>
        </div>

        <div className={`form-group ${categories.touched && categories.invalid ? 'has-danger' : ''}`}>
          <label>Categories</label>
          <input type="text" className="form-control" {...categories}/>
          {categories.touched ? categories.error : ''}
        </div>

        <div className={`form-group ${content.touched && content.invalid ? 'has-danger' : ''}`}>
          <label>Content</label>
          <textarea className="form-control" {...content}/>
          {content.touched ? content.error : ''}
        </div>

        <button type="submit" className="btn btn-primary">Submit</button>
        <Link to="/" className="btn btn-danger"></Link>
      </form>
    );
  };
}

function validate(values){
  const errors = {};
  if(!values.title){
    errors.title = 'Enter a user name';
  }

  return errors;
}


//reduxForm: 1st is form config, 2nd is mapStateToProps, 3rd is mapDispatchToProps
export default reduxForm({
  form:'PostsNewForm',
  fields: ['title', 'categories', 'content']
}, null, {createPost})(PostsNew);





// state ==={
//   form:{
//     PostsNewForm:{
//       title:'...',
//       categories:'...',
//       content:'...'
//     }
//   }
// }

