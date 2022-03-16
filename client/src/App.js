import React, { useContext } from 'react'
import {
  BrowserRouter as Routes,
  Route,
  Switch,
  Redirect
} from "react-router-dom";
import { routes } from './routes'
import { AuthPage } from './pages/AuthPage'
import { Layout } from './layout'
import { AuthContext } from './context/AuthProvider'
import { url } from './constants'

function PrivateRoute({ user, ...route }) {
  if (route.permissons.includes(user.resources)) {
    return (
      <Route
        path={route.path}
        {...route.exact}
        render={props => (
          <route.component {...props} />
        )}
      />
    )
  }
}

const App = () => {
  const { user } = useContext(AuthContext)
  return (
    <Routes>
      {
        (user) ?
          (<Layout>
            <Switch>
              {
                routes.map(route => <PrivateRoute key={route.id} user={user} {...route} />)
              }
              <Redirect to={`${url}/calls`} />
            </Switch>
          </Layout>) : (<>
            <Route path={`${url}/login`} exact>
              <AuthPage />
            </Route>
            <Redirect to={`${url}/login`} />
          </>)
      }
    </Routes>
  )
}

export default App
